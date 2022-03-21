using UnityEngine;

namespace SpaceGame
{
    static class Crosshair
    {
        static float _x, _y;
        static Vector2 _direction2D;
        static float _halfScreenWidth = Screen.width / 2;
        static float _halfScreenHeight = Screen.height / 2;
        static float _deadZonePercent = 0.80f;
        static float _deadZone = _halfScreenHeight * _deadZonePercent;
        static float _maxMagnitudePow2 = Mathf.Pow(_deadZone, 2) * 2; //константа, причем зависит от полей выше, корректно ее так задать?
                
        public static void RecalculateParameters()
        {
            //можно через конструктор new vector, как быстрее для производительности?
            _direction2D.x = Input.mousePosition.x - _halfScreenWidth;
            _direction2D.y = Input.mousePosition.y - _halfScreenHeight;

            if (_direction2D.magnitude > _deadZone)
            {
                _direction2D = _direction2D.normalized * _deadZone;
            }
        }
        
        static public Vector2 Direction2D
        {
            get {return _direction2D;}
        }

        static public float Magnitude2DToDeadzonePercent
        {
            get { return _direction2D.magnitude / _deadZone; }
        }


        static public Quaternion ShipDesiredRotation
        {
            get
            {                
                return Quaternion.LookRotation(new Vector3(_direction2D.x, _direction2D.y, _halfScreenHeight));
            }
        }

        static public Quaternion TurretDesiredRotation
        {
            get
            {
                //можно вставить прямо в return в функцию quaternion, присвоение переменных минус производительность но плюс понятность кода, как лучше?
                float vectorZ = Mathf.Sqrt(_maxMagnitudePow2 - _direction2D.x * _direction2D.x - _direction2D.y * _direction2D.y);
                return Quaternion.LookRotation(new Vector3(_direction2D.x, _direction2D.y, vectorZ)).normalized;
            }
        }

    }
}


