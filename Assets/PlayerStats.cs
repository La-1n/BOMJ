using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public RectTransform hpBar;
    public RectTransform hungerBar;

    public Slider hpSlider;      // ������� ��� ����������� ��������
    public Slider hungerSlider;  // ������� ��� ����������� ������
    public Slider thirstSlider;  // ������� ��� ����������� �����
    public Slider experienceSlider;   // ������� ��� ����������� ������

    public Text hpText;      // ��������� ���� ��� ����������� ��������
    public Text hungerText;  // ��������� ���� ��� ����������� ������
    public Text thirstText;  // ��������� ���� ��� ����������� �����
    public Text levelText;  // ��������� ���� ��� ����������� ������

    public float hp;      // ������� ��������
    public float maxHp;   // ������������ ��������
    public float hunger;  // ������� ������� ������
    public float maxHunger; // ������������ ������� ������
    public float thirst;  // ������� ������� �����
    public float maxThirst; // ������������ ������� �����

    public int level = 1; // ������� �������
    public int currentExp = 0; // ������� ���������� �����
    public int expToLevelUp = 10; // ���������� �����, ����������� ��� ��������� ������

    // ����������� ������� ������
    string[] names = { "�������", "��������", "��������������", "������ ����", "���������� �� ���������", "������ ���������", "������ ����", "������� ����", "���� ���������" };
    int[] exps = { 100, 300, 600, 1000, 1500, 2100, 2800, 3600, 4500 };
    int[] healths = { 22, 24, 26, 28, 30, 32, 34, 36, 38 };
    int[] hungriness = { 21, 22, 23, 24, 25, 26, 27, 28, 29 };
    int[] thirstiness = { 21, 22, 23, 24, 25, 26, 27, 28, 29 };

    void Start()
    {
        // ������������� ������������ �������� ��� ���������
        hpSlider.maxValue = maxHp;
        hungerSlider.maxValue = maxHunger;
        thirstSlider.maxValue = maxThirst;
        experienceSlider.maxValue = expToLevelUp;
    }

    void Update() => StatsManage();

    //����� �������� �������������
    void StatsManage()
    {
        // ������������ ����������
        hp = Mathf.Clamp(hp, 0, maxHp);
        hunger = Mathf.Clamp(hunger, 0, maxHunger);
        thirst = Mathf.Clamp(thirst, 0, maxThirst);

        // ��������� �������� �� ���������
        hpSlider.value = (int)hp;
        hungerSlider.value = (int)hunger;
        thirstSlider.value = (int)thirst;

        // ��������� �������� �� ��������� �����
        hpText.text = ((int)hp).ToString();
        hungerText.text = ((int)hunger).ToString();
        thirstText.text = ((int)thirst).ToString();

        // ���������, �� ���� �� �����
        if ((int)hp == 0)
        {
            Die();
        }
    }

    // ����� ��� ��������� �����
    public void GainExp(int exp)
    {
        currentExp += exp;
        if (currentExp >= expToLevelUp) // ���� ���������� ����� ��� ��������� ������
        {
            LevelUp();
        }

        // ��������� ������� �����
        experienceSlider.value = currentExp;
    }

    // ����� ��� ��������� ������
    public void LevelUp()
    {
        currentExp -= expToLevelUp; // �������� �� �������� ���������� ����� ����������� ��� ��������� ������
        expToLevelUp = exps[level - 1]; // ���������, ������� ���������� ����� ��� ��������� �� ���������� ������

        // �������� ��������� � ���������
        RectTransform rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x + thirstiness[level - 1] - maxThirst, rect.sizeDelta.y);

        hpBar.sizeDelta = new Vector2(hpBar.sizeDelta.x + healths[level - 1] - maxHp, hpBar.sizeDelta.y);
        hungerBar.sizeDelta = new Vector2(hungerBar.sizeDelta.x + hungriness[level - 1] - maxHunger, hungerBar.sizeDelta.y);

        // ����������� ���������� ������ � ����������� �� ������
        maxHp = healths[level - 1];
        maxHunger = hungriness[level - 1];
        maxThirst = thirstiness[level - 1];
        levelText.text = names[level - 1];

        // ������������� ������������ �������� ��� ���������
        hpSlider.maxValue = maxHp;
        hungerSlider.maxValue = maxHunger;
        thirstSlider.maxValue = maxThirst;
        experienceSlider.value = currentExp;
        experienceSlider.maxValue = expToLevelUp;

        level++; // ����������� �������

        // ������� ��������� � ��������� ������
        Debug.Log("�� �������� " + level + " ������! ���� ���������� ����������: ������������ �������� = " + maxHp + ", ������������ ����� = " + maxHunger + ", ������������ ����� = " + maxThirst);
    }

    // ����� ��� ������
    void Die()
    {
        // �������� ����� ���, ������� ����� ����������� ��� ������ ������
        Debug.Log("Game Over");
    }

    // ����� ��� �������������� ��������
    public void RestoreHp(float amount)
    {
        hp += amount;

        // ������������ �������� �������� ������������ ���������
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    // ����� ��� ���������� ������ ������
    public void IncreaseHunger(float amount)
    {
        hunger += amount;

        // ������������ �������� ������ ������ ������������ ���������
        if (hunger > maxHunger)
        {
            hunger = maxHunger;
        }
    }

    // ����� ��� ���������� ������ �����
    public void IncreaseThirst(float amount)
    {
        thirst += amount;

        // ������������ �������� ������ ����� ������������ ���������
        if (thirst > maxThirst)
        {
            thirst = maxThirst;
        }
    }
}