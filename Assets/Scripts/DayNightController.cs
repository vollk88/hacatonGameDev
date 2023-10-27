using BaseClasses;
using UnityEngine;

public class DayNightController : CustomBehaviour
{
	public float dayLengthInSeconds = 600.0f; // Длительность дня в секундах

	[GetOnObject]
	private Light _sunLight;
	
	private float _timeOfDay; // Время суток в секундах

	private void Start()
	{
		_sunLight.color = Color.white; // Устанавливаем начальное освещение (день)
	}

	private void Update()
	{
		// Увеличиваем время суток
		_timeOfDay += Time.deltaTime / dayLengthInSeconds;

		// Пересчитываем угол солнца
		float sunAngle = Mathf.Lerp(-90, 270, _timeOfDay);
		_sunLight.transform.rotation = Quaternion.Euler(sunAngle, 0, 0);

		// Переключение между днем и ночью
		if (!(_timeOfDay >= 1.0f)) return;
		_timeOfDay = 0.0f;
		// Измените цвет света для смены дня и ночи
		_sunLight.color = _sunLight.color == Color.white ? Color.black : Color.white;
	}
}