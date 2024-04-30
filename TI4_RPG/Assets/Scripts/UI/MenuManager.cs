using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : Sigleton<MenuManager> {
    public Menu CurrentMenu;

    
    public void OpenMenu(Menu menu) {
        CurrentMenu.OnCloseMenu();
        menu.OnOpenMenu();
        CurrentMenu = menu;
    }
}
