using System;

    public class HealthPoints 
    {
        public event Action OnNoHP;
        public event Action<float> OnChange;

        private float _maxValue;
        private float _currentValue;

        public HealthPoints(float maxHP)
        {
            _maxValue = _currentValue = maxHP;
        }

        public void ChangeHP(float valueChange)
        {
            _currentValue += valueChange;

            if(_currentValue > _maxValue)
            {
                _currentValue =_maxValue;
            }

            if(_currentValue > 0)
            {
                OnChange?.Invoke(_currentValue/_maxValue);
            }
            else
            {
                _currentValue = 0;
                OnNoHP?.Invoke();
            }
        }
    }
