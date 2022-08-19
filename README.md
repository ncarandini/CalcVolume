# CalcVolume
Calcola il volume di riempimento di un pavimento non planare a partire da una griglia di misurazione.

Ad esempio, per una griglia di 5 righe e 11 colonne:

![Alt text](/drawings/Misure.drawio.svg?sanitize=true)

preso un riferimento di livello (tipicamente con apparecchiatura laser) si pone un'asta graduata in corrispondenza di ogni punto e si annota la corrispondente misura.

Si ottiene una matrice di dati che per semplicità verrà direttamente inserita nel codice sorgente:

``` csharp
// Le misure  di altezza sono espresse in centimetri
double[,] misureAltezza =
{
    { 13.0, 14.1, 15.5, 14.0, 15.0, 15.0, 16.0, 15.0, 14.8, 14.0, 13.9 },
    { 13.0, 14.0, 14.7, 14.5, 15.0, 15.0, 15.0, 14.7, 14.5, 14.7, 14.5 },
    { 11.0, 13.0, 13.5, 13.1, 14.3, 14.0, 13.5, 13.0, 12.8, 13.4, 13.6 },
    { 09.8, 12.0, 13.0, 13.3, 13.0, 13.4, 13.0, 13.0, 12.2, 13.2, 13.4 },
    { 10.2, 11.7, 12.0, 12.5, 12.7, 12.7, 12.8, 13.3, 12.9, 12.9, 12.9 }
};
```

Inoltre andranno inserite le coordinate posizioni dei punti di misura dove per convenzione il punto di misura pm(0,0) corrisponde all'origine del piano cartesiano.

Nel suddetto esempio i punti sono tutti distanziati di 1 metro, tranne l'ultima colonna che si trova a 9,47 metri di distanza, quindi si ha:

``` csharp
// Le posizioni sono espresse in metri
double[] posizioniX = { 0.0, 1.0, 2.0, 3.0, 4.0 };
double[] posizioniY = { 0.0, 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 9.47 };
```

Infine, poiché l'autolivellante deve stare al di sopra del punto più alto misurato, è possibile definire nel sorgente anche tale spessore:

```  csharp
// Lo spessore aggiuntivo è espresso in centimetri
double spessoreAggiuntivo = 1.0;
```
Se invece si vuol calcolare solo il volume di riempimento della parte non planare, basta impostare tale volume a zero.

Una volta impostati i dati, è sufficiente eseguire il programma per o9ttenere in console il valore del volume, espresso in metri cubi.
