using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public RectTransform hpBar;
    public RectTransform hungerBar;

    public Slider hpSlider;      // слайдер для отображения здоровья
    public Slider hungerSlider;  // слайдер для отображения голода
    public Slider thirstSlider;  // слайдер для отображения жажды
    public Slider experienceSlider;   // слайдер для отображения уровня

    public Text hpText;      // текстовое поле для отображения здоровья
    public Text hungerText;  // текстовое поле для отображения голода
    public Text thirstText;  // текстовое поле для отображения жажды
    public Text levelText;  // текстовое поле для отображения уровня

    public float hp;      // текущее здоровье
    public float maxHp;   // максимальное здоровье
    public float hunger;  // текущий уровень голода
    public float maxHunger; // максимальный уровень голода
    public float thirst;  // текущий уровень жажды
    public float maxThirst; // максимальный уровень жажды

    public int level = 1; // текущий уровень
    public int currentExp = 0; // текущее количество опыта
    public int expToLevelUp = 10; // количество опыта, необходимое для повышения уровня

    // необходимые массивы данных
    string[] names = { "Бродяга", "Прохожий", "Путешественник", "Житель улиц", "Специалист по выживанию", "Мастер выживания", "Король улиц", "Легенда улиц", "Гуру выживания" };
    int[] exps = { 100, 300, 600, 1000, 1500, 2100, 2800, 3600, 4500 };
    int[] healths = { 22, 24, 26, 28, 30, 32, 34, 36, 38 };
    int[] hungriness = { 21, 22, 23, 24, 25, 26, 27, 28, 29 };
    int[] thirstiness = { 21, 22, 23, 24, 25, 26, 27, 28, 29 };

    void Start()
    {
        // устанавливаем максимальное значение для слайдеров
        hpSlider.maxValue = maxHp;
        hungerSlider.maxValue = maxHunger;
        thirstSlider.maxValue = maxThirst;
        experienceSlider.maxValue = expToLevelUp;
    }

    void Update() => StatsManage();

    //метод контроля характеристик
    void StatsManage()
    {
        // ограничиваем переменные
        hp = Mathf.Clamp(hp, 0, maxHp);
        hunger = Mathf.Clamp(hunger, 0, maxHunger);
        thirst = Mathf.Clamp(thirst, 0, maxThirst);

        // обновляем значения на слайдерах
        hpSlider.value = (int)hp;
        hungerSlider.value = (int)hunger;
        thirstSlider.value = (int)thirst;

        // обновляем значения на текстовых полях
        hpText.text = ((int)hp).ToString();
        hungerText.text = ((int)hunger).ToString();
        thirstText.text = ((int)thirst).ToString();

        // проверяем, не умер ли игрок
        if ((int)hp == 0)
        {
            Die();
        }
    }

    // метод для получения опыта
    public void GainExp(int exp)
    {
        currentExp += exp;
        if (currentExp >= expToLevelUp) // если достаточно опыта для повышения уровня
        {
            LevelUp();
        }

        // обновляем слайдер опыта
        experienceSlider.value = currentExp;
    }

    // метод для повышения уровня
    public void LevelUp()
    {
        currentExp -= expToLevelUp; // вычитаем из текущего количества опыта необходимое для повышения уровня
        expToLevelUp = exps[level - 1]; // указываем, сколько необходимо опыта для повышения до следующего уровня

        // внесение изменений в интерфейс
        RectTransform rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x + thirstiness[level - 1] - maxThirst, rect.sizeDelta.y);

        hpBar.sizeDelta = new Vector2(hpBar.sizeDelta.x + healths[level - 1] - maxHp, hpBar.sizeDelta.y);
        hungerBar.sizeDelta = new Vector2(hungerBar.sizeDelta.x + hungriness[level - 1] - maxHunger, hungerBar.sizeDelta.y);

        // увеличиваем показатели игрока в зависимости от уровня
        maxHp = healths[level - 1];
        maxHunger = hungriness[level - 1];
        maxThirst = thirstiness[level - 1];
        levelText.text = names[level - 1];

        // устанавливаем максимальное значение для слайдеров
        hpSlider.maxValue = maxHp;
        hungerSlider.maxValue = maxHunger;
        thirstSlider.maxValue = maxThirst;
        experienceSlider.value = currentExp;
        experienceSlider.maxValue = expToLevelUp;

        level++; // увеличиваем уровень

        // выводим сообщение о повышении уровня
        Debug.Log("Вы достигли " + level + " уровня! Ваши показатели улучшились: максимальное здоровье = " + maxHp + ", максимальный голод = " + maxHunger + ", максимальная жажда = " + maxThirst);
    }

    // метод для СМЕРТИ
    void Die()
    {
        // добавьте здесь код, который будет выполняться при смерти игрока
        Debug.Log("Game Over");
    }

    // метод для восстановления здоровья
    public void RestoreHp(float amount)
    {
        hp += amount;

        // ограничиваем значение здоровья максимальным значением
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    // метод для увеличения уровня голода
    public void IncreaseHunger(float amount)
    {
        hunger += amount;

        // ограничиваем значение уровня голода максимальным значением
        if (hunger > maxHunger)
        {
            hunger = maxHunger;
        }
    }

    // метод для увеличения уровня жажды
    public void IncreaseThirst(float amount)
    {
        thirst += amount;

        // ограничиваем значение уровня жажды максимальным значением
        if (thirst > maxThirst)
        {
            thirst = maxThirst;
        }
    }
}