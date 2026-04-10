# Spanzuratoarea

Aplicație realizată în C# folosind WPF și arhitectura MVVM.
Aplicația implementează jocul clasic Spânzurătoarea, permițând utilizarea mai multor conturi, salvarea jocurilor și vizualizarea statisticilor.

## Funcționalități

### Utilizatori
- Creare utilizator (nume + imagine)
- Selectare utilizator existent
- Ștergere utilizator (inclusiv datele asociate)

### Joc
- Alegere categorie de cuvinte
- Cuvânt ales aleator
- Afișare progres
- Introducere litere:
  - din interfață
  - de la tastatură
- Literele nu pot fi refolosite

### Sistem joc
- Timp de 30 secunde pentru fiecare cuvânt, unde sunt permise maxim 6 greșeli
- Afișare spânzurătoare progresivă
- Jocul se câștigă după 3 niveluri consecutive

### Salvare și încărcare
- Salvare joc în fișiere JSON
- Încărcare joc doar pentru utilizatorul curent

### Statistici
- Număr jocuri jucate
- Număr jocuri câștigate

## Tehnologii
- C#
- WPF (.NET Core)
- MVVM
- Data Binding
- ICommand
- JSON

## Structură
- Models – date (User, GameState)
- ViewModels – logică
- Views – interfață
- Services – salvare, statistici, cuvinte
- Commands – RelayCommand

## Rulare
1. Deschide proiectul în Visual Studio
2. Rulează aplicația (F5)
