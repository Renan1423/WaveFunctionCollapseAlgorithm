using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void SetOnFocus(bool focused);

    public void OnInteract(GameObject interactionOwner);
}
