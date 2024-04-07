using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Work : MonoBehaviour
{
    static private int _minValue;
    static private int _maxValue;
    static private int _middleNumber;
    static private int _tempCount;
    static private string _moreOrLess;

    [SerializeField] private GameObject _settingUI;
    [SerializeField] private Button _openSettingButtom;
    [SerializeField] private Button _closeSettingButtom;
    [SerializeField] private Button _restartSettingButtom;
    [Space]
    [SerializeField] private GameObject _pregameScreen;
    [SerializeField] private Button _playButtom;
    [Space]
    [SerializeField] private GameObject _startScreen;
    [SerializeField] private Button _readyButtom;
    [Space]
    [SerializeField] private GameObject _gameScreen;
    [SerializeField] private TextMeshProUGUI _guessText;
    [SerializeField] private Button _rightButtom;
    [SerializeField] private Button _falseButtom;
    [Space]
    [SerializeField] private GameObject _resultScreen;
    [SerializeField] private TextMeshProUGUI _resultNumber;
    [SerializeField] private Button _guessedButtom;
    [SerializeField] private Button _notGuessedButtom;
    [Space]
    [SerializeField] private GameObject _finishScreen;
    [SerializeField] private TextMeshProUGUI _finishText;
    [SerializeField] private Button _restartFinidhButtom;


    private void Start()
    {
        OnStartOrRestartButtonClic();
        _restartSettingButtom.onClick.AddListener(OnStartOrRestartButtonClic);
        _restartFinidhButtom.onClick.AddListener(OnStartOrRestartButtonClic);
        _openSettingButtom.onClick.AddListener(OnOpenSettingButtonClic);
        _closeSettingButtom.onClick.AddListener(OnCloseSettingButtonClic);
        _playButtom.onClick.AddListener(OnPlayButtonClic);
        _readyButtom.onClick.AddListener(OnReadyButtonClic);
        _guessText.text = $"Ваше число {_moreOrLess} {_middleNumber}?";
        _rightButtom.onClick.AddListener(OnGuessNumberRight);
        _falseButtom.onClick.AddListener(GuessNumberFalse);
        _guessedButtom.onClick.AddListener(OnGuessedNumberButtonClic);
        _notGuessedButtom.onClick.AddListener(OnNotGuessedNumberButtonClic);
    }

    private void OnOpenSettingButtonClic()
    {
        _settingUI.SetActive(true);
    }

    private void OnCloseSettingButtonClic()
    {
        _settingUI.SetActive(false);
    }

    private void OnStartOrRestartButtonClic()
    {
        _minValue = 0;
        _maxValue = 100;
        _middleNumber = (_minValue + _maxValue) / 2;
        _tempCount = 0;
        _guessText.text = $"Ваше число {_moreOrLess} {_middleNumber}?";
        _settingUI.SetActive(false);
        _gameScreen.SetActive(false);
        _resultScreen.SetActive(false);
        _finishScreen.SetActive(false);
        _pregameScreen.SetActive(false);
        _startScreen.SetActive(true);
    }

    private void OnPlayButtonClic()
    {
        _startScreen.SetActive(false);
        _pregameScreen.SetActive(true);
    }

    private void OnReadyButtonClic()
    {
        _pregameScreen.SetActive(false);
        _gameScreen.SetActive(true);
    }

    private void OnGuessedNumberButtonClic()
    {
        _resultScreen.SetActive(false);
        _finishScreen.SetActive(true);
        _finishText.text = $"Я угадал! \n Уходи!";
    }

    private void OnNotGuessedNumberButtonClic()
    {
        _resultScreen.SetActive(false);
        _finishScreen.SetActive(true);
        _finishText.text = $"Ты врешь! \n угадал!";
    }

    private void OnGuessNumberRight()
    {
        GuessNumber(true);

        if (_maxValue - _minValue < 2)
        {
            _guessText.text = $"Ваше число четное?";
            _tempCount++;
        }
        else
        {
            _moreOrLess = "больше";
            _guessText.text = $"Ваше число {_moreOrLess} {_middleNumber}?";
        }
    }

    private void GuessNumberFalse()
    {
        GuessNumber(false);

        if (_maxValue - _minValue < 2)
        {
            _guessText.text = $"Ваше число нечетное?";
            _tempCount--;
        }
        else
        {
            _moreOrLess = "меньше";
            _guessText.text = $"Ваше число {_moreOrLess} {_middleNumber}?";
        }
    }

    private void GuessNumber(bool value)
    {
        if ((value && _tempCount > 0) || (!value && _tempCount < 0))
        {
            _gameScreen.SetActive(false);
            _resultScreen.SetActive(true);

            if (_minValue % 2 == 0)
            {
                _resultNumber.text = $"Ваше число {_minValue}?";
            }
            else
            {
                _resultNumber.text = $"Ваше число {_maxValue}?";
            }
        }
        else if ((!value && _tempCount > 0) || (value && _tempCount < 0))
        {
            _gameScreen.SetActive(false);
            _resultScreen.SetActive(true);

            if (_maxValue % 2 == 0)
            {
                _resultNumber.text = $"Ваше число {_minValue}?";
            }
            else
            {
                _resultNumber.text = $"Ваше число {_maxValue}?";
            }
        }
        else if ((value && _moreOrLess == "меньше") || (!value && _moreOrLess == "больше"))
        {
            _maxValue = _middleNumber;
            _middleNumber = (_maxValue + _minValue) / 2;
        }
        else
        {
            _minValue = _middleNumber + 1;
            _middleNumber = (_maxValue + _minValue) / 2;
        }
    }
}