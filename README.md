# Programowanie Genetyczne
Repozytorium na potrzeby Programowania Genetycznego Informatyki i Systemów Inteligentnych, 5 semestr

## Sharp GP
### Content
- SharpGP/SharpGP_Structures - zawiera wszystkie struktury danych używane w projekcie
  - /Tree - zawiera struktury obiektów ewolucji, czyli programów
  - /Grammar - zawiera pliki Antlerowe oraz naszą gramatykę
  - /Generator - zawiera naszą implementację Antlerowego Visitora, który generuje Abstract Syntax Tree, czyli konwertuje ze stringa do PRogramu
  - /Evolution - zawiera struktury do zapisywania przebiegu ewolucji
  - ProgramRunContext - Struktura która jest RAM'em dla naszego programu. Zawiera dane we/wyjścia oraz metryki.
- SharpGP/SharpGP - zawiera skrypty związane z samym procesem ewolucji
  - /Program.cs - trigger do głównego skryptu. Pobiera rzeczy z /TestSuites i wrzuca wyniki do /Results
  - /TestSuites - zawiera pliki testowe do wytrenowania. Każdy plik to instancja klasy `TestSet`
  - /Results - zawiera wyniki trenowania. Każdy plik to instancja klasy `EvolutionHistory`
  - /Utils/TreeGenerator.cs - zawiera funkcje do generowania drzew na różne sposoby
  - /SharpGP.cs - główny skrypt, zawierający core procesu trenowania
  - /SharpGP_Crossover.cs - skrypt pomocniczy zawierający funkcję decydującą o elemencie crossującym. Funkcja implementuje Fair-size Crossover
  - /SolutionTester.cs - pozwala na szybkie zajrzenie do wyników. Skrypt pobieraja wyniki z Result i uruchamia programy z poszczególnych etapów, tak żeby móc zobaczyć output najlepszego programu z każdej epoki.
### Code smells
- Plan był taki żeby nic nie edytowało drzewa z zewnątrz - żeby nie dało się zepsuć struktury. Nauczyłem każdy element mutacji i grow, ale w przypadku crossowania, nie chciałem żeby drzewo samo się crossowało. Dlatego na samym końcu musiałem wyprowadzić referencję do listy node'ów na zewnątrz `PRogram, l.18: public List<Node> GetNodes() => Nodes;` Można się zastanowić nad naprawą tej niekonsystencji
- SharpGP/Program.cs pobiera EvolutionHistory, ale powinien go zapisywać częściej, bo nie zawsze trenowanie się uda.
- SharpGP.cs funkcja PerformEvolution. To była ostatnia funkcja pisana w tym projekcie. Została zrobiona na szybko, są w środku rzeczy whardkodowane, a cała funkcja powinna być rozbita na podfunkcje dla przejrzystości. Przydałaby się też struktura ogólnego procesu ewolucji, żeby z funkcji wewnętrznych, po postawieniu np. breakpointa, było wiadomo np. która generacja jest liczona.
- SharpGP_crossover.cs linijka 23. Kiedy brakuje albo node'ów mniejszych albo większych, powinien być wybrany o takim samym rozmiarze. Jednak zwracany jest random, ponieważ zwrócenie nulla rzucało exception i tak było łatwiej to załatać. 
- EvolutionHistory zawiera sobie listy zamiast agregat. O ile przy 100 osobnikach w populacji może nie wygląda to na konieczne, tak na (docelowo) większych populacjach wystarczy zebrać średnią, medianę, kilka percentyli, może też odchylenie standardowe i inne.
- AntlrToProgram.cs w linijce 19 jest Substring(2). Nie pamiętam dlaczego tak jest, ale był do tego powód. Najlepiej zostawić wszystkie konsekutywne cyfry.
- Mamy większą kontrolę nad problemami, kiedy możemy zedytować funkcję oceniającą dokładnie jednego z nich. Jednak ostatecznie nie zakończyło się to dobrze. Bardzo dużo etapów współdzieliło funkcje oceniające.
- Nie wszystkie funkcje są poprawnie skonfigurowane do przykładów! W trakcie kopiowania wkradły się błędy.
- Funkcje `GetTypeToCross`, ... powinny przyjmować listę typów istniejących nodeów, i rozdystrybuować prawdopodobieństwo pomiędzy nie, a następnie zwrócić któryś z istniejących elementów
- I pewnie parę innych, ale nie miałem czasu już tego przeglądać. Rozpoczął się nowy rok akademicki i jak znalazłeś to repo to pewnie go potrzebujesz, także częstuj się :)

## TinyGP
Ten paragraf powstał jako rozwiązanie zadania na potrzeby samego przedmiotu i nie ma większego związku z esencją repozytorium.
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
- TinyGP/Results/Generated/\*/Simplified - zawiera uproszczenia każdego z problemów w postaci przyjaznej dla excela
  - pliki z dopiskiem result zawierają uproszczone formuły
  - pliki z dopiskiem time zawierają czas upraszczania w sekundach
- TinyGP/Results/Generated/\*/Logs\* zawiera wynik z konsoli, które zawierają m.in. informację o ilości generacji i czasie obliczeń
- TinyGP/Data/*.xlsx - zawierają zagregowane wyniki ewolucji z danej konfiguracji
  Na końcu należy ręcznie, przy pomocy excela przekopiować zawartość plików csv do plików .xlsx. Wykresy zostaną zaktualizowane automatycznie

### Podsumowanie zadania 1 i zadania dodatkowego
Ze względu na automatyzację procesu ewolucji, TinyGP był uruchamiany w każdej z 3 konfiguracji (Basic, Basic+Sin+Cos, Basic+Exp) dla wszystkich zadanych problemów.
Trening był przyspieszony poprzez ograniczenie ilości generacji do 40, dlatego wyniki nie są idealne, ale zwykle nakreślają nadany w pliku dat cel, z wyjątkiem gdzie funkcja wystrzeliła z wartościami i dawała same wartości inf, które trzeba było pominąć. Zwykle się składało tak, że funkcja miała albo same normalne wartości, albo same inf. Ze względu na konwencjonalność rozwiązania, zamiast wprowadzać równania do excela, ostatni pipeline robi ewaluację i generuje csv z samych wartości.

Obejrzenie materiałów załączonych do zadania, następnie ulepszenie TinyGP bez zrozumienia\* i napisanie pipeline'ów zajęło **10h** zegarowych.

\* bez zrozumienia - kod da ulepszyć po prostu dopisując case'y tam gdzie były wypisane operacje bezmyślnie i kod się uruchamia. Co więcej, wyniki są całkiem sensowne, mimo tego że ciekawe rzeczy działy się pod spodem. To co prezentują excele aktualnie na repozytorium to wynik działania właśnie tego kodu.<br/>
Jakie tam były bugi? Funkcje jednoargumentowe były traktowane jako dwuargumentowe, w efekcie przy krzyżowaniu zabierały one ze sobą najbliższe prawe poddrzewo, jeżeli nie bezpośredniego rodzica, to kolejnego. To sprawia że przy wklejeniu takiego genu w środek innego drzewa, efekt jest bardzo zbliżony do przelosowania prawych poddrzew wszystkich rodziców nowo doklejonego węzła.<br/>
Błąd został wykryty w ramach przygotowania do następnych zadań, które wymagają znajomości procesu krzyżowania i mutacji drzew na poziomie atomowym. Te przygotowania, zaowocowały znalezieniem błędu, a analiza zachowania tego kodu wraz z analizą skutków błedu zajęła **5h** zegarowych i, co gorsza, była niezbędna do poprawnego rozwiązania poprzedniego zadania, które i tak zajęło dużo czasu.
