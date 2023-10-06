using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGunTypes 
{
    void Shoot(KeyCode shootKey);
    void ChangeGun(Transform gunPlace, so_GunParameters parameters);
    void ShootAnim();// ToDo
}
