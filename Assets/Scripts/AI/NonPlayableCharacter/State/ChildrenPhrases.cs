using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace AI.NonPlayableCharacter.State
{
	[CreateAssetMenu(fileName = "ChildrenPhrases", menuName = "AI/ChildrenPhrases")]
	public class ChildrenPhrases : ScriptableObject
	{
		[TextArea(25, 8)]
		[SerializeField] private string phrases;

		public ReadOnlyCollection<string> Phrases
		{
			get
			{
				string[] phrasesArray = phrases.Split('\n');
				return phrasesArray != null ? new ReadOnlyCollection<string>(phrasesArray) : null;
			}
		}
	}
}