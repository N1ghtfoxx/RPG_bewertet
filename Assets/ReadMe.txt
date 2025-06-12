Erweiterte Punktemöglichkeiten zum Projekt "PRG_bewertet"



Truhenanimation
-> zu finden in ChestManager.cs

Zeile 70-89: public void OpenChest()



Unterschiedliche Item-Typen
-> zu finden in BaseItem.cs

Zeile 10: Festlegen, dass der itemTyp in Unity auch ausgewählt werden kann
+
Zeile 46-53: Definition der Item-Typen



Persistenz(Speichern und Laden)

Speichern und Laden des Inventars
-> zu finden in CharacterStatsManager.cs

Zeile 16-20: Definition FileName und FilePath
Zeile 106-167: private void SaveInventory() und private void LoadInventory()


Speichern und Laden des Truhenzustands
-> zu finden in ChestManager.cs

Zeile 16-19: Definition der Klasse zum speichern des Truhenzustands und des Truheninventars + Variable für den Pfad
Zeile 64-115: private void OpenChest() und private void LoadChestData() ( speichere und lade den Zustand der Truhe in eine und aus einer JSON-Datei )
Zeile 128-135: Serializable public class ChestSaveData definiert die Klasse, die den Zustand der Truhe speichert