// Copyright (C) 2022 Nicolò Carandini

// Lo spessore aggiuntivo è espresso in centimetri
double spessoreAggiuntivo = 1.0;

// Le misure  di altezza sono espresse in centimetri
double[,] misureAltezza =
{
    { 13.0, 14.1, 15.5, 14.0, 15.0, 15.0, 16.0, 15.0, 14.8, 14.0, 13.9 },
    { 13.0, 14.0, 14.7, 14.5, 15.0, 15.0, 15.0, 14.7, 14.5, 14.7, 14.5 },
    { 11.0, 13.0, 13.5, 13.1, 14.3, 14.0, 13.5, 13.0, 12.8, 13.4, 13.6 },
    { 09.8, 12.0, 13.0, 13.3, 13.0, 13.4, 13.0, 13.0, 12.2, 13.2, 13.4 },
    { 10.2, 11.7, 12.0, 12.5, 12.7, 12.7, 12.8, 13.3, 12.9, 12.9, 12.9 }
};

// Le posizioni sono espresse in metri
double[] posizioniX = { 0.0, 1.0, 2.0, 3.0, 4.0 };
double[] posizioniY = { 0.0, 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 9.47 };

// Crea la griglia di punti di misura
PuntoMisura[,] misureGriglia = new PuntoMisura[misureAltezza.GetLength(0),misureAltezza.GetLength(1)];
for (int r = 0; r < misureAltezza.GetLength(0); r++)
{
    for (int c = 0; c < misureAltezza.GetLength(1); c++)
    {
        misureGriglia[r,c] = new PuntoMisura(posizioniX[r], posizioniY[c], misureAltezza[r,c]);
    }
}

double volumeGriglia = VolumeGriglia(spessoreAggiuntivo);
Console.WriteLine($"Il volume è pari a {volumeGriglia} mc");

// Funzioni ------------------------------------------------

// I volumi sono espressi in metri cubi
double VolumeGriglia(double spessoreAggiuntivo = 0.0)
{
    double volume = 0;
    double misuraMinima = CalcMisuraMinima();

    for (int r = 0; r < misureGriglia.GetLength(0) - 1; r++)
    {
        for (int c = 0; c < misureGriglia.GetLength(1) - 1; c++)
        {
            volume += VolumePrismaTriangolareObliquo(
                misureGriglia[r,c],
                misureGriglia[r,c+1],
                misureGriglia[r+1,c],
                misuraMinima);

            volume += VolumePrismaTriangolareObliquo(
                misureGriglia[r,c],
                misureGriglia[r,c+1],
                misureGriglia[r+1,c],
                misuraMinima);
        }
    }

    if (spessoreAggiuntivo > 0)
    {
        volume += AreaGriglia() * spessoreAggiuntivo / 100;
    }

    return volume;
}

// La misura minima è espressa in centimetri
double CalcMisuraMinima()
{
    double min = double.MaxValue;
    foreach (double m in misureAltezza)
    {
        min = Math.Min(min, m);
    }
    return min;
}

// Le superfici sono espresse in metri quadri
double AreaGriglia()
{
    double minX = double.MaxValue;
    double maxX = 0;

    double minY = double.MaxValue;
    double maxY = 0;

    foreach (PuntoMisura pm in misureGriglia)
    {
        minX = Math.Min(minX, pm.X);
        maxX = Math.Max(maxX, pm.X);
        minY = Math.Min(minY, pm.Y);
        maxY = Math.Max(maxY, pm.Y);
    }
    return (maxX - minX) * (maxY - minY);
}

// I volumi sono espressi in metri cubi
double VolumePrismaTriangolareObliquo(PuntoMisura pm1, PuntoMisura pm2, PuntoMisura pm3, double misuraMinima)
{
    // Calcolo dell'area di un triangolo definito da tre punti sul piano cartesiano
    // https://www.studenti.it/matematica/area-di-un-triangolo-noti-i-vertici-15.jspc
    double areaDiBase = 0.5 * Math.Abs(
        pm1.X * pm2.Y + pm1.Y * pm3.X + pm2.X * pm3.Y
        - pm3.X * pm2.Y - pm3.Y * pm1.X - pm2.X * pm1.Y
    );

    return areaDiBase * ((pm1.Z + pm2.Z + pm3.Z) / 100) / 3;
}


