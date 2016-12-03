using UnityEngine;
using System.Collections;

public class tankHealth : MonoBehaviour
{

    public float mFullHealth = 100f;

    private float mCurrentHealth;
    private bool mAlive;

    private void Awake()
    {

    }

    private void OnEnable()
    {
        mCurrentHealth = mFullHealth;
        mAlive = true;
        SetHealthUI();
    }

    public void TakeDamage(float number)
    {
        mCurrentHealth -= number;
        SetHealthUI();
        if (mCurrentHealth <= 0f && mAlive == true)
        {
            OnDeath();
        }
    }
    private void SetHealthUI()
    {

    }
    private void OnDeath()
    {
        mAlive = false;

        gameObject.SetActive(false);
    }
    public float HPpercent()
    {
        return mCurrentHealth / mFullHealth;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
