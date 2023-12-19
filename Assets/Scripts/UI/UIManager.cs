using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _playerCoins;
    private int _coinsCount;

    public void Init()
    {
        _playerCoins.text = string.Format("Coins: {0}", _coinsCount);
    }
    public void AddCoins(int value)
    {
        _coinsCount += value;
        _playerCoins.text = string.Format("Coins: {0}",_coinsCount);
    }
}
