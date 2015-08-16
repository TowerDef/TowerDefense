using UnityEngine;
using System.Collections;

public enum AIState
{
		IDLE,
		MOVING,
		ATTACKING,
		DYING
}

public class UnitAI : MonoBehaviour
{
		public AIState aiState;
		
		public AnimationClip[] idleAnimations;
		public AnimationClip[] walkAnimations;
		public AnimationClip[] attackAnimations;
		public AnimationClip[] dieAnimations;
		
		private UnitProperties unitProperties;
		private WaypointManager wayPointManager;
		private GameManager gameManager;
		
		private WayPointPath wayPointPath;
		private int creepSpawnerIndex;
	
		private int currentWayPoint;
		private Vector3 targetWayPoint;
		private float minDistanceToWayPoint = 0.5f;
		
		private float attackSpeed;

		private Vector3 facingDirection = Vector3.zero;

		private void Awake ()
		{
				unitProperties = this.gameObject.GetComponent<UnitProperties> ();
				wayPointManager = FindObjectOfType (typeof(WaypointManager)) as WaypointManager;
				gameManager = FindObjectOfType (typeof(GameManager)) as GameManager;
		}
		
		private void Start ()
		{
				wayPointPath = wayPointManager.wayPointPaths [creepSpawnerIndex];
				attackSpeed = unitProperties.attackSpeed;
				
				aiState = AIState.IDLE;		
		}
		
		private void Update ()
		{
				UpdateHealthState ();
				UpdateAnimationState ();
				if (aiState != AIState.DYING) {
						UpdateFacingDirection ();
						UpdateMovementPosition ();
						UpdateDamageCalculation ();
				}
		}

		private void UpdateFacingDirection ()
		{
				facingDirection = targetWayPoint - this.transform.position;
				
				// If direction is not zero, rotate towards direction (via rotateDegreesPerSecond).
				if (facingDirection != Vector3.zero) {
						// Take care of direction being directly behind our current forward vector since the 
						// Quaternion.RotateTowards will continuously alternate between right and left rotations to get there,
						// resulting in never getting there, just wiggling around the current transform.rotation.
						if (Vector3.Angle (transform.forward, facingDirection) > 179) {
								// This will cause us to always turn to the right to go the opposite direction
								facingDirection = transform.TransformDirection (new Vector3 (.01f, 0, -1));
						}
						
						this.transform.rotation = Quaternion.RotateTowards (this.transform.rotation, Quaternion.LookRotation (facingDirection), unitProperties.rotateDegreesPerSecond * Time.deltaTime);
				}
		}
    
		private void UpdateMovementPosition ()
		{
				if (currentWayPoint != wayPointPath.wayPoints.Length && Vector3.Distance (this.transform.position, wayPointPath.wayPoints [wayPointPath.wayPoints.Length - 1].transform.position) > minDistanceToWayPoint) {
						aiState = AIState.MOVING;
						
						targetWayPoint = wayPointPath.wayPoints [currentWayPoint].transform.position;
						if (Vector3.Distance (this.transform.position, targetWayPoint) < minDistanceToWayPoint) {
								currentWayPoint++;
						}
			
						//TODO: Instant rotation.
						//Vector3 unitDirection = (targetWayPoint - this.transform.position);
						//unitDirection.Normalize();
						//this.gameObject.transform.position += unitDirection * unitProperties.movementSpeed / 100 * Time.deltaTime;
                        
						//TODO:Smooth rotation.
						Vector3 unitDirection = transform.forward;
						this.gameObject.transform.position += unitDirection * unitProperties.movementSpeed / 100 * Time.deltaTime;
				} else {
						aiState = AIState.ATTACKING;
				}
		}
    
		private void UpdateDamageCalculation ()
		{		
				if (aiState == AIState.ATTACKING) {
						attackSpeed -= 1.0f * Time.deltaTime;
						if (attackSpeed <= 0) {
								attackSpeed = unitProperties.attackSpeed;
								gameManager.DecreasePlayerHealth (unitProperties.damagePoints);
						}
				}
		}
    
		private void UpdateHealthState ()
		{
				if (aiState == AIState.DYING) {
						return;
				}
		
				if (unitProperties.healthPoints <= 0) {
						aiState = AIState.DYING;
						
						unitProperties.DestroyUnit ();
						gameManager.IncreasePlayerGold (unitProperties.goldGainOnDeath);
				}
		}
    
		private void UpdateAnimationState ()
		{
				if (aiState == AIState.IDLE) {
						animation.wrapMode = WrapMode.Default;
						
						//If an animation in this AIState is already playing, wait for it to finish. Then Crossfade to another animation, be it the same AIState or another AIState.
						for (int i = 0; i < idleAnimations.Length; i++) {
								if (animation.IsPlaying (idleAnimations [i].name)) {
										return;
								}
						}
						
						animation.CrossFade (idleAnimations [Random.Range (0, idleAnimations.Length)].name);
            
				} else if (aiState == AIState.MOVING) {
						animation.wrapMode = WrapMode.Default;
						
						for (int i = 0; i < walkAnimations.Length; i++) {
								if (animation.IsPlaying (walkAnimations [i].name)) {
										return;
								}
						}
            
						animation.CrossFade (walkAnimations [Random.Range (0, walkAnimations.Length)].name);
            
				} else if (aiState == AIState.ATTACKING) {
						animation.wrapMode = WrapMode.Default;
						
						for (int i = 0; i < attackAnimations.Length; i++) {
								if (animation.IsPlaying (attackAnimations [i].name)) {
										return;
								}
						}
            
						animation.CrossFade (attackAnimations [Random.Range (0, attackAnimations.Length)].name);
            
				} else if (aiState == AIState.DYING) {
						animation.wrapMode = WrapMode.ClampForever;
						animation.CrossFade (dieAnimations [Random.Range (0, dieAnimations.Length)].name);
				}
		}
		
		public void DecreaseHealth (int healthPoints)
		{
				unitProperties.healthPoints -= healthPoints;
		}
		
		public int CreepSpawnerIndex { get { return creepSpawnerIndex; } set { creepSpawnerIndex = value; } }
}
