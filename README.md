Programm zum Prüfen, ob die Temp Tables in einem Dataset korrekt includiert, definiert und einer relationship zugeordnet sind.
# Kommandozeilenargumente

-i [Pfad zu PDS-Datei]

# Beispiel Ausgabe:

Informationen zu Dataset:[Pfad zur PDS-Datei]

---

TempTable-Includes :89

TempTable-Defines :84

TempTable-Relationships :83

---

Folgende TTs sind includiert aber nicht definiert

Folgende TTs sind definiert aber nicht includiert

Folgende TTs sind definiert aber nicht in den relationships

Folgende TTs sind in relationships aber nicht definiert

Folgende TTs sind in relationships aber nicht includiert

---

Anzahl an FirstLevel-TempTables 1

Anzahl an distinct Child-TempTables 83
