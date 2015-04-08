using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class CardState {
	public int attack;
	public int health;
	public int time;
	public int cost;
	public bool ranged;
	public bool block;
	public bool speed;
	public bool hidden;
	public string name;
	public string description;
	public string effect;
	public string type;
	
	// temporary
	public Material cardMaterial;
	public Material pieceMaterial;
	public string color;
}

public static class Cards {

	public static string[] types = new string[] {"Human", "Robot", "Fishfolk", "Warhound", "Spirit", "Spell"};
	public static string[] colors = new string[] {"neutral", "red", "blue", "green"};

	// Neutral Cards
	
	// Young Adventurer
	public static CardState YoungAdventurer = new CardState {
		attack = 1,
		health = 2,
		time = 8,
		cost = 1,
		ranged = false,
		block = false,
		speed = false,
		hidden = false,
		name = "Young Adventurer",
		description = "",
		effect = "",
		type = types[0],
		cardMaterial = (Material) Resources.Load("materials/card/adventurer_card", typeof(Material)),
		pieceMaterial = (Material) Resources.Load("materials/piece/adventurer_piece", typeof(Material)),
		color = colors[0]
	};

	// Gunslinger
	public static CardState Gunslinger = new CardState {
		attack = 3,
		health = 1,
		time = 7,
		cost = 2,
		ranged = true,
		block = false,
		speed = false,
		hidden = false,
		name = "Gunslinger",
		description = "",
		effect = "Range",
		type = types[0],
		cardMaterial = (Material) Resources.Load("materials/card/gunslinger_card", typeof(Material)),
		pieceMaterial = (Material) Resources.Load("materials/piece/gunslinger_piece", typeof(Material)),
		color = colors[0]
	};
	
	// Shieldbearer
	public static CardState Shieldbearer = new CardState {
		attack = 2,
		health = 4,
		time = 9,
		cost = 3,
		ranged = false,
		block = true,
		speed = false,
		hidden = false,
		name = "Shieldbearer",
		description = "",
		effect = "Block",
		type = types[0],
		cardMaterial = (Material) Resources.Load("materials/card/shieldbearer_card", typeof(Material)),
		pieceMaterial = (Material) Resources.Load("materials/piece/shieldbearer_piece", typeof(Material)),
		color = colors[0]
	};
	
	// Red Cards
	
	// Warhound Scout
	public static CardState WarhoundScout = new CardState {
		attack = 2,
		health = 1,
		time = 7,
		cost = 1,
		ranged = false,
		block = false,
		speed = true,
		hidden = false,
		name = "Warhound Scout",
		description = "",
		effect = "Speed",
		type = types[3],
		cardMaterial = (Material) Resources.Load("materials/card/warhound_card", typeof(Material)),
		pieceMaterial = (Material) Resources.Load("materials/piece/warhound_piece", typeof(Material)),
		color = colors[1]
	};

	// Warhound Soldier
	public static CardState WarhoundSoldier = new CardState {
		attack = 1,
		health = 2,
		time = 6,
		cost = 2,
		ranged = false,
		block = false,
		speed = false,
		hidden = false,
		name = "Warhound Soldier",
		description = "",
		effect = "Has +1/+0 for each Warhound in play.",
		type = types[3],
		cardMaterial = (Material) Resources.Load("materials/card/red_common_card", typeof(Material)),
		pieceMaterial = (Material) Resources.Load("materials/piece/red_common_piece", typeof(Material)),
		color = colors[1]
	};
	
	// Warhound Captain
	public static CardState WarhoundCaptain = new CardState {
		attack = 5,
		health = 2,
		time = 7,
		cost = 4,
		ranged = false,
		block = false,
		speed = false,
		hidden = false,
		name = "Warhound Captain",
		description = "",
		effect = "Shout: Summon two Warhounds",
		type = types[3],
		cardMaterial = (Material) Resources.Load("materials/card/red_common_card", typeof(Material)),
		pieceMaterial = (Material) Resources.Load("materials/piece/red_common_piece", typeof(Material)),
		color = colors[1]
	};
	
	// Hound Master
	public static CardState HoundMaster = new CardState {
		attack = 5,
		health = 4,
		time = 8,
		cost = 5,
		ranged = false,
		block = false,
		speed = false,
		hidden = false,
		name = "Hound Master",
		description = "",
		effect = "All Warhounds have Speed.",
		type = types[0],
		cardMaterial = (Material) Resources.Load("materials/card/red_uncommon_card", typeof(Material)),
		pieceMaterial = (Material) Resources.Load("materials/piece/red_uncommon_piece", typeof(Material)),
		color = colors[1]
	};
	
	// Emperor Cezar
	public static CardState EmperorCezar = new CardState {
		attack = 6,
		health = 4,
		time = 5,
		cost = 6,
		ranged = false,
		block = false,
		speed = false,
		hidden = false,
		name = "Emperor Cezar",
		description = "",
		effect = "All Warhounds have +1/+1 and Speed.",
		type = types[3],
		cardMaterial = (Material) Resources.Load("materials/card/cezar_card", typeof(Material)),
		pieceMaterial = (Material) Resources.Load("materials/piece/cezar_piece", typeof(Material)),
		color = colors[1]
	};
	
	// Blue Cards
	
	// Fishfolk Drifter
	public static CardState FishfolkDrifter = new CardState {
		attack = 3,
		health = 1,
		time = 9,
		cost = 2,
		ranged = false,
		block = false,
		speed = false,
		hidden = true,
		name = "Fishfolk Drifter",
		description = "",
		effect = "Hidden",
		type = types[2],
		cardMaterial = (Material) Resources.Load("materials/card/fishfolk_card", typeof(Material)),
		pieceMaterial = (Material) Resources.Load("materials/piece/fishfolk_piece", typeof(Material)),
		color = colors[2]
	};
	
	// Sentrybot
	public static CardState SentryBot = new CardState {
		attack = 1,
		health = 5,
		time = 4,
		cost = 3,
		ranged = false,
		block = false,
		speed = false,
		hidden = false,
		name = "Sentrybot",
		description = "",
		effect = "",
		type = types[1],
		cardMaterial = (Material) Resources.Load("materials/card/robot_card", typeof(Material)),
		pieceMaterial = (Material) Resources.Load("materials/piece/robot_piece", typeof(Material)),
		color = colors[2]
	};

	// Arcane Mechanic
	public static CardState ArcaneMechanic = new CardState {
		attack = 3,
		health = 3,
		time = 8,
		cost = 4,
		ranged = false,
		block = false,
		speed = false,
		hidden = false,
		name = "Arcane Mechanic",
		description = "",
		effect = "Shout: Draw a card.",
		type = types[0],
		cardMaterial = (Material) Resources.Load("materials/card/mechanic_card", typeof(Material)),
		pieceMaterial = (Material) Resources.Load("materials/piece/mechanic_piece", typeof(Material)),
		color = colors[2]
	};

	// Dweller Shaman
	public static CardState DwellerShaman = new CardState {
		attack = 5,
		health = 5,
		time = 8,
		cost = 5,
		ranged = false,
		block = false,
		speed = false,
		hidden = false,
		name = "Dweller Shaman",
		description = "",
		effect = "The next card you play is Hidden.",
		type = types[2],
		cardMaterial = (Material) Resources.Load("materials/card/shaman_card", typeof(Material)),
		pieceMaterial = (Material) Resources.Load("materials/piece/shaman_piece", typeof(Material)),
		color = colors[2]
	};
	
	// Tidea
	public static CardState Tidea = new CardState {
		attack = 5,
		health = 9,
		time = 8,
		cost = 8,
		ranged = true,
		block = false,
		speed = false,
		hidden = false,
		name = "Tidea",
		description = "",
		effect = "Range. All non-fishfolk creature timers last twice as long.",
		type = types[2],
		cardMaterial = (Material) Resources.Load("materials/card/tidea_card", typeof(Material)),
		pieceMaterial = (Material) Resources.Load("materials/piece/tidea_piece", typeof(Material)),
		color = colors[2]
	};
	
	// Green Cards
	
	// Leafling
	public static CardState Leafling = new CardState {
		attack = 1,
		health = 1,
		time = 7,
		cost = 2,
		ranged = false,
		block = false,
		speed = false,
		hidden = false,
		name = "",
		description = "",
		effect = "",
		type = types[4],
		cardMaterial = (Material) Resources.Load("materials/card/leafling_card", typeof(Material)),
		pieceMaterial = (Material) Resources.Load("materials/piece/leafling_piece", typeof(Material)),
		color = colors[3]
	};
	
	// Shroomsprite
	public static CardState Shroomsprite = new CardState {
		attack = 1,
		health = 4,
		time = 8,
		cost = 2,
		ranged = false,
		block = false,
		speed = false,
		hidden = false,
		name = "",
		description = "",
		effect = "",
		type = types[4],
		cardMaterial = (Material) Resources.Load("materials/card/shroomsprite_card", typeof(Material)),
		pieceMaterial = (Material) Resources.Load("materials/piece/shroomsprite_piece", typeof(Material)),
		color = colors[3]
	};
	
	// Demigod
	public static CardState Demigod = new CardState {
		attack = 9,
		health = 9,
		time = 9,
		cost = 9,
		ranged = false,
		block = false,
		speed = false,
		hidden = false,
		name = "",
		description = "",
		effect = "",
		type = types[4],
		cardMaterial = (Material) Resources.Load("materials/card/demigod_card", typeof(Material)),
		pieceMaterial = (Material) Resources.Load("materials/piece/demigod_piece", typeof(Material)),
		color = colors[3]
	};
	
	// Decks
	public static List<CardState> BlueDeck = new List<CardState>() {
		CloneCard(YoungAdventurer),
		CloneCard(YoungAdventurer),
		CloneCard(Gunslinger),
		CloneCard(Gunslinger),
		CloneCard(Tidea),
		CloneCard(FishfolkDrifter),
		CloneCard(FishfolkDrifter),
		CloneCard(DwellerShaman),
		CloneCard(DwellerShaman),		
	};
	
	public static List<CardState>[] decks = new List<CardState>[] {BlueDeck,};
	
	
	private static CardState CloneCard(CardState card) {
		if (card == null) return card;
		return new CardState {
			attack = card.attack,
			health = card.health,
			time = card.time,
			cost = card.cost,
			ranged = card.ranged,
			block = card.block,
			speed = card.speed,
			hidden = card.hidden,
			name = card.name,
			description = card.description,
			effect = card.effect,
			type = card.type,
			cardMaterial = card.cardMaterial,
			pieceMaterial = card.pieceMaterial,
			color = card.color
		};
	}
}