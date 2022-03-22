using UnityEngine;
using UnityEngine.UI;

namespace SpaceGame
{
    public class PlayerShip : MonoBehaviour, ITakeDamage, IHealable
    {
        [SerializeField] private GameObject _weaponBarrelObject;
        [SerializeField] private GameObject _mainCamera;
        [SerializeField] private GameObject _engineTurboFlame;
        [SerializeField] private GameObject _engineFlame;
        [SerializeField] private GameObject _shieldPrefab;
        [SerializeField] private GameObject _laserPrefab;
        [SerializeField] private GameObject _asteroidPrefab;
        [SerializeField] private Transform _shieldSpawnPosition;
        [SerializeField] private Transform _laserSpawnPosition;
        [SerializeField] private Text _GUITextPresenter; //методология описывает такой случай именования? все же 3 буквы здесь акроним                                                         
        private float _playerShipHP = 100;
        private float _playerShipMaxHP = 100;
        private float _playerShipMaxSpeedNormal = 150;
        private float _playerShipMaxSpeedBoosted = 300f;
        private float _playerShipTurnSpeed = 40f;
        private float _playerShipRollSpeed = 40f;
        private float _playerShipSpeed = 0;
        private float _playerShipStrafeSpeed = 50;
        private float _playerShipAcceleration = 3;
        private float _playerShipAccelerationBoosted = 5;
        private float _lastFiringTime = -10;
        private float _weaponMaxCharge = 50;
        private float _weaponCharge = 50;
        private float _weaponChargeSpeedNormal = 0.02f;
        private float _weaponChargeSpeedBoosted = 0.4f;
        private float _weaponChargeBoostDelay = 2f;
        private float _weaponFireDelay = 0.075f;
        private bool _fireOn;
        private bool _shieldOn;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) 
                _shieldOn = true;
            if (Input.GetKey(KeyCode.Mouse0))
                _fireOn = true;            
        }

        private void FixedUpdate()
        {            
            Crosshair.RecalculateParameters();
            
            float TurningSpeed = Crosshair.Magnitude2DToDeadzonePercent * _playerShipTurnSpeed;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.rotation * Crosshair.ShipDesiredRotation, TurningSpeed * Time.fixedDeltaTime);
            transform.rotation *= Quaternion.Euler(0, 0f, Input.GetAxis("Roll") * _playerShipRollSpeed * Time.fixedDeltaTime);
            MovePlayerShip(Time.fixedDeltaTime);

            _weaponBarrelObject.transform.rotation = transform.rotation * Crosshair.TurretDesiredRotation;           

            if (_weaponCharge < _weaponMaxCharge)
                _weaponCharge += (Time.time - _lastFiringTime) > _weaponChargeBoostDelay ? _weaponChargeSpeedBoosted : _weaponChargeSpeedNormal;
            
            if (_shieldOn)
            {
                _shieldOn = false;
                SpawnShield();
            }

            if (_fireOn)
            {
                if (Time.time - _lastFiringTime > _weaponFireDelay && _weaponCharge >= 1)
                    Fire();
                _fireOn = false;
                
            }

            _GUITextPresenter.text = $"Ammo: {_weaponCharge:N0}\nHealth: {_playerShipHP/_playerShipMaxHP*100:N0}%\nSpeed: {(_playerShipSpeed / 10):N0} m/s";
        }

        private void SpawnShield()
        {
            var PlayerShieldObj = Instantiate(_shieldPrefab, _shieldSpawnPosition.position, _shieldSpawnPosition.rotation);
            var PlayerShieldScript = PlayerShieldObj.GetComponent<PlayerShield>();
            PlayerShieldScript.Init(100f);
            PlayerShieldObj.transform.SetParent(_shieldSpawnPosition);          
        }

        private void MovePlayerShip(float delta)
        {            
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.Tab))
                {
                    _engineTurboFlame.SetActive(true);

                    if (_playerShipSpeed + _playerShipAccelerationBoosted < _playerShipMaxSpeedBoosted)
                    {
                        _playerShipSpeed += _playerShipAccelerationBoosted;
                    }
                    else _playerShipSpeed = _playerShipMaxSpeedBoosted;
                }
                else
                {
                    _engineTurboFlame.SetActive(false);
                    if (_playerShipSpeed + _playerShipAcceleration < _playerShipMaxSpeedNormal)
                    {
                        _playerShipSpeed += _playerShipAcceleration;
                    }
                    else
                    {
                        if (_playerShipSpeed - _playerShipAcceleration > _playerShipMaxSpeedNormal)
                        {
                            _playerShipSpeed -= _playerShipAcceleration;
                        }
                        else
                        {
                            _playerShipSpeed = _playerShipMaxSpeedNormal;
                        }
                    }
                }
            }
            else
            {
                _engineTurboFlame.SetActive(false);
                if (_playerShipSpeed - _playerShipAcceleration > 0)
                    _playerShipSpeed -= _playerShipAcceleration;
                else _playerShipSpeed = 0;
            }

            if (_playerShipSpeed > 0) _engineFlame.SetActive(true);
            else _engineFlame.SetActive(false);

            //transform.position += transform.forward * _playerShipSpeed * delta + transform.right * Input.GetAxis("HorizontalX") * _playerShipStrafeSpeed * delta;
            transform.Translate(transform.forward * _playerShipSpeed * delta + transform.right * Input.GetAxis("HorizontalX") * _playerShipStrafeSpeed * delta, Space.World);
        }
                private void Fire()
        {
            _lastFiringTime = Time.time;
            _weaponCharge--;
            Instantiate(_laserPrefab, _laserSpawnPosition.position, _laserSpawnPosition.rotation);
        }

        public void Hit(float damage)
        {
            if (_playerShipHP > 0)
            {
                _playerShipHP -= damage;
            }            
            if (_playerShipHP <= 0)
            {
                print("Вы проиграли!");
            }
        }

        public void Heal()
        {
            _playerShipHP = _playerShipMaxHP;            
        }

    }
}