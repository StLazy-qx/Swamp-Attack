using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _sellButton;

    //храним урожие, которое продаем
    private Weapon _weapon;

    //если это продажа, то мы передаем наш weapon
    //WeaponView передаем, так как отписаться не могут от события
    //говорит, отпишись от меня если продажа удастся 
    public event UnityAction<Weapon, WeaponView> SellButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
        //происходит закрытие кнопки после ее нажатия
        _sellButton.onClick.AddListener(TryLockItem);

    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
        _sellButton.onClick.RemoveListener(TryLockItem);
    }

    private void TryLockItem()
    {
        //отключаем взаимодействия с кнопкой
        _sellButton.interactable = false;
    }

    //метод отрисовки объекта
    public void Render(Weapon weapon)
    {
        //произойдет установка оружия и занесение данных
        _weapon = weapon;

        _label.text = weapon.Label;
        _price.text = weapon.Price.ToString();
        _icon.sprite = weapon.Icon;
    }

    private void OnButtonClick()
    {
        //будет что то происходить по покупке
        //this ,передаем себя
        SellButtonClick?.Invoke(_weapon, this);
    }
}
