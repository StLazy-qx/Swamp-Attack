using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Player _player;
    //обзор всего оружия
    [SerializeField] private WeaponView _template;
    //куда создаем объекты, где будут расположенны товары
    [SerializeField] private GameObject _itemContainer;

    private void Start()
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            AddItem(_weapons[i]);
        }
    }

    private void AddItem(Weapon weapon)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.SellButtonClick += OnSellButtonClick;
        view.Render(weapon);
    }

    //если кто то нажал на кнопку покупки
    //обработка нажатия
    private void OnSellButtonClick(Weapon weapon, WeaponView view)
    {
        //что именно делаем
        TrySellWeapon(weapon, view);
    }

    // что после нажатия нужно сделать
    private void TrySellWeapon(Weapon weapon, WeaponView view)
    {
        if (weapon.Price <= _player.Money)
        {
            _player.BuyWeapon(weapon);
            //метод, что оружие куплено
            weapon.Buy();
            view.SellButtonClick -= OnSellButtonClick;
        }
    }
}
