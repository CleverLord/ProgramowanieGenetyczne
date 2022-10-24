# Programowanie Genetyczne
Repozytorium na potrzeby Programowania Genetycznego Informatyki i Systemów Inteligentnych, 5 semestr

## TinyGP
### Content
Repozytorium składa się z 4 pipeline'ów. 
- TinyGP/Data/DataGenerator.py - Służy do wygenerowania plików .dat
- TinyGP/src/tiny_gp.java - Wykonuje algorytm programowania genetycznego
- TinyGP/Data/SolutionOptimizer.ipynb - Optymalizuje wynik działania tinyGP przy pomocy scipy
- TinyGP/Data/ExcelMerger.ipynb - generuje pliki .csv

Struktura folderów
- TinyGP/Data - zawiera pliki na których pracuje TinyGP oraz pliki pomocnicze ("pythony")
- TinyGP/pythonData - zawiera pliki .dat wygenerowane przez pytona
- TinyGP/Results/Generated - zawiera wyniki ewolucji każdego z problemów. Podfolder Generated jest pozostałością po ręcznym przeglądaniu logów
- TinyGP/Results/Generated/\*/Simplified - zawiera uproszczenia każdego z problemów dokonanego przez pythona
  - pliki z dopiskiem result zawierają uproszczone formuły
  - pliki z dopiskiem time zawierają czas upraszczania w sekundach
- TinyGP/Results/Generated/\*/Logs\* zawiera wynik z konsoli, które zawierają m.in. informację o ilości generacji i czasie obliczeń
- TinyGP/Data/*.xlsx - zawierają zagregowane wyniki ewolucji z danej konfiguracji
Na końcu należy ręcznie, przy pomocy excela przekopiować zawartość plików csv do plików .xlsx. Wykresy zostaną zaktualizowane automatycznie

### Podsumowanie zadania 1 i zadania dodatkowego
Ze względu na automatyzację procesu ewolucji, TinyGP był uruchamiany w każdej z 3 konfiguracji (Basic, Basic+Sin+Cos, Basic+Exp) dla wszystkich zadanych problemów.
Trening był przyspieszony poprzez ograniczenie ilości generacji do 40, dlatego wyniki nie są idealne, ale zwykle nakreślają nadany w pliku dat cel, z wyjątkiem gdzie funkcja wystrzeliła z wartościami i dawała same wartości inf, które trzeba było pominąć. Zwykle się składało tak, że funkcja miała albo same normalne wartości, albo same inf.

Na ten etap: obejrzenie materiałów zapewnionych przez prowadzącego, następnie ulepszenie TinyGP bez zrozumienia\* i napisanie pipeline'ów zajęło **10h** zegarowych.

\* bez zrozumienia - kod da ulepszyć po prostu dopisując case'y tam gdzie były wypisane operacje bezmyślnie i kod się uruchamia. Co więcej, wyniki są całkiem sensowne, mimo tego że ciekawe rzeczy działy się pod spodem. To co prezentują excele aktualnie na repozytorium to wynik działania właśnie tego kodu.<br/>
Jakie tam były bugi? Funkcje jednoargumentowe były traktowane jako dwuargumentowe, w efekcie przy krzyżowaniu zabierały one ze sobą najbliższe prawe poddrzewo, jeżeli nie bezpośredniego rodzica, to kolejnego. To sprawia że przy wklejeniu takiego genu w środek innego drzewa, efekt jest bardzo zbliżony do przelosowania prawych poddrzew wszystkich rodziców nowo doklejonego węzła.<br/>
Błąd został wykryty w ramach przygotowania do następnych zadań, które wymagają znajomości procesu krzyżowania i mutacji drzew na poziomie atomowym. Te przygotowania, zaowocowały znalezieniem błędu, a analiza zachowania tego kodu wraz z analizą skutków błedu zajęła **5h** zegarowych i, co gorsza, była niezbędna do poprawnego rozwiązania poprzedniego zadania, które i tak zajęło dużo czasu.
