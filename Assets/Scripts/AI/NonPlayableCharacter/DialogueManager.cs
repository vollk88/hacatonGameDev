using System;
using System.Collections.Generic;
using System.Linq;
using AI.NonPlayableCharacter.State;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace AI.NonPlayableCharacter
{
	[Serializable]
	public class DialogueManager : BaseClasses.CustomBehaviour
	{
		[SerializeField] 
		private ChildrenPhrases childrenPhrases;
		
		private static readonly Dictionary<string, float> PhraseOdds = new();

		private const float DecreaseFactor = 0.1f;
		
		[SerializeField]
		private TextMeshProUGUI text;
		
		public void Start()
		{
			if (PhraseOdds.Count != 0)
				return;
			
			var phrases = childrenPhrases.Phrases.ToArray();
			
			foreach (var phrase in phrases)
			{
				PhraseOdds[phrase] = 1f / phrases.Length;
			}
		}

		public void UpdatePhrase()
		{
			text.text = GetPhrase();
		}
		
		public string GetPhrase()
		{
			// Выбор фразы на основе вероятности
			float total = 0;
			float randomPoint = Random.value * PhraseOdds.Values.Sum();
			foreach (var entry in PhraseOdds)
			{
				total += entry.Value;
				if (total >= randomPoint)
				{
					// уменьшаем вероятность выбранной фразы
					PhraseOdds[entry.Key] *= (1 - DecreaseFactor);
					NormalizeOdds();
					return entry.Key;
				}
			}
			return null;
		}

		private void NormalizeOdds()
		{
			float sumOdds = PhraseOdds.Values.Sum();
			var keys = new List<string>(PhraseOdds.Keys);
			foreach (var key in keys)
			{
				PhraseOdds[key] /= sumOdds;
			}
		}
	}
}