using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MobDetails")]
public class MobDetails : ScriptableObject {

    [SerializeField] private string mobName;
    [SerializeField] private float speed;
    [SerializeField] private int maxHealth;

    public string MobName => mobName;
    public float Speed => speed;
    public int MaxHealth => maxHealth;
    
}
