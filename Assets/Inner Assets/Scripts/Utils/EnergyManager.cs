using System;
using System.Collections;
using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    // #region ПЕРЕМЕННЫЕ
    // private bool isFocusWasLost = false;
    //
    // [HideInInspector]
    // public readonly int energyTimerLenhgt = 280; // длительность получения одной энергии в секундах
    // [HideInInspector]
    // public int count_secondsForNextEnergyTimer = 0;
    // #endregion
    //
    // private DataManager DataManager;
    //
    // public static EnergyManager Instance;
    // private void Awake()
    // {
    //     Instance = this;
    // }
    //
    // private void Start()
    // {
    //     DataManager = DataManager.Instance;
    //     CheckTimeForEnergy();
    // }
    //
    // private void OnApplicationFocus(bool focus)
    // {
    //     // Если фокус был потерян (это нужно чтобы при первом запуске этот код не запускался, т.к все уже было инициализировано в awake)
    //     if (focus == false)
    //     {
    //         StopCoroutine("EnergyAddingTimer");
    //         int timerWorkTime = energyTimerLenhgt - count_secondsForNextEnergyTimer;
    //         // Прошлая сесия = время - время работы таймера
    //         PlayerPrefs.SetString("LAST_SESION", DateTime.UtcNow.AddSeconds(-timerWorkTime).ToString());
    //         PlayerPrefs.Save();
    //         isFocusWasLost = true;
    //     }
    //     // Если это не первый запуск то тогда проверка
    //     else if (isFocusWasLost)
    //     {
    //         CheckTimeForEnergy();
    //     }
    // }
    //
    // // При входе проверяет время отсутвия игрока и дает исходя из этого энергию
    // // Запускает таймер на получение энергии если это нужно
    // private void CheckTimeForEnergy()
    // {
    //     string lastSession = PlayerPrefs.GetString("LAST_SESION", DateTime.UtcNow.ToString());
    //     TimeSpan timeBetwenNowAndLastSession = DateTime.UtcNow - DateTime.Parse(lastSession);
    //     int count_totalSeconds = (int)timeBetwenNowAndLastSession.TotalSeconds;
    //     #region Добавление энергии      
    //     int count_energy = DataManager.GetEnergyCount();
    //     int count_maxEnergy = DataManager.MAX_ENERGY_COUNT;
    //     // Проверка на то надо ли добавлять энергию (если энергии меньше чем макс количество и время с прошлой сессии больше секунды
    //     if (count_energy < count_maxEnergy && count_totalSeconds > 0)
    //     {
    //         // Разницу между входом и выходом из игры в секундах делю на время генерации одной энергии
    //         float count_energyForAdding = (float)count_totalSeconds / energyTimerLenhgt;
    //         // Новое количество энергии
    //         count_energy += (int)Math.Floor(count_energyForAdding);
    //         // Если энергия полностью заполнилась
    //         if (count_energy >= count_maxEnergy)
    //         {
    //             DataManager.Set_EnergyCount(count_maxEnergy);
    //         }
    //         else
    //         {
    //             DataManager.Set_EnergyCount(count_energy);
    //         }
    //
    //         // ЗАПУСК ТАЙМЕРА
    //         // Если энергии меньше макс.количества и разницы хватило для добавления энергии
    //         if (count_energy < count_maxEnergy &&
    //             count_totalSeconds > energyTimerLenhgt)
    //         {
    //             // разница сесий - (максимально время таймера * количесво полученной энергии за разинцу сесий)) 
    //             int i = count_totalSeconds - (energyTimerLenhgt * (int)Math.Floor(count_energyForAdding));
    //             // Время до след получения энергии
    //             count_secondsForNextEnergyTimer = energyTimerLenhgt - i;
    //             StartCoroutine("EnergyAddingTimer");
    //         } // Если разницы не хватило для добавления энергии
    //         else if (count_energy < count_maxEnergy && count_totalSeconds < energyTimerLenhgt)
    //         {
    //             count_secondsForNextEnergyTimer = energyTimerLenhgt - count_totalSeconds;
    //             StartCoroutine("EnergyAddingTimer");
    //         }
    //     }
    //     #endregion
    // }
    //
    // public IEnumerator EnergyAddingTimer()
    // {
    //     int count_maxEnergy = DataManager.MAX_ENERGY_COUNT;
    //     while (true)
    //     {
    //         count_secondsForNextEnergyTimer--;
    //         // Если таймер закончился
    //         if (count_secondsForNextEnergyTimer == 0)
    //         {
    //             DataManager.Set_EnergyCount(DataManager.GetEnergyCount() + 1);
    //             // Таймер сбрасываеться на 30 минут
    //             count_secondsForNextEnergyTimer = energyTimerLenhgt;
    //             // Если энергия полная то корутина завершаеться
    //             if (DataManager.GetEnergyCount() >= count_maxEnergy) yield break;
    //         }
    //         yield return new WaitForSeconds(1);
    //     }
    // }
    //
    // // Перезапуск таймера с временем для энергии с нуля
    // public void Resset_EnergAddingTimer()
    // {
    //     // Обнуления таймера энергии
    //     StopCoroutine("EnergyAddingTimer");
    //     count_secondsForNextEnergyTimer = energyTimerLenhgt;
    //     StartCoroutine("EnergyAddingTimer");
    // }
    //
    // // Полное отключение таймера
    // public void Disable_EnergAddingTimer()
    // {
    //     StopCoroutine("EnergyAddingTimer");
    //     count_secondsForNextEnergyTimer = energyTimerLenhgt;
    // }
}
