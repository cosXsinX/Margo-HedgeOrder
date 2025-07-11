# Margo.HedgePricer

## Description

Margo.HedgePricer est une application de simulation de trading qui génère des cotations (quotes) aléatoires et les convertit en ordres de trading. Le projet utilise une architecture basée sur des canaux (channels) pour la communication asynchrone entre les composants producteurs et consommateurs.

### Architecture

- **Margo.HedgePricer.Core** : Bibliothèque principale contenant les modèles et la logique métier
- **Margo.HedgePricer.Console** : Application console pour exécuter la simulation

### Composants principaux

- **QuoteEmitter** : Génère des cotations aléatoires à intervalles réguliers
- **RandomQuoteFactory** : Crée des cotations avec des prix et spreads configurables
- **OrderConsumer** : Consomme les cotations et les convertit en ordres
- **QuoteToOrderConverter** : Convertit les cotations en ordres de trading

## Prérequis

- **.NET 8.0 SDK** ou version ultérieure
- **Visual Studio 2022** ou **Visual Studio Code** (recommandé)
- **Windows 10/11** ou **Linux/macOS**

### Installation de .NET 8.0

1. Téléchargez le SDK .NET 8.0 depuis [dotnet.microsoft.com](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Suivez les instructions d'installation pour votre système d'exploitation
3. Vérifiez l'installation en exécutant : `dotnet --version`

## Commandes d'exécution

### Restaurer les dépendances

```bash
dotnet restore
```

### Compiler le projet

```bash
dotnet build
```

### Exécuter l'application

```bash
dotnet run --project Margo.HedgePricer.Console
```


### Nettoyer les fichiers de build

```bash
dotnet clean
```

## Structure du projet

```
Margo.HedgePricer/
├── Margo.HedgePricer.Console/     # Application console
├── Margo.HedgePricer.Core/        # Bibliothèque principale
│   ├── Consumer/                   # Composants consommateurs
│   ├── Model/                      # Modèles de données
│   └── Producer/                   # Composants producteurs
```

## Configuration

L'application peut être configurée en modifiant les paramètres dans `Program.cs` :

- **Instrument** : Paire de devises (ex: "EUR/USD")
- **Prix de base** : Prix de référence pour les cotations
- **Spread** : Écart entre les prix d'achat et de vente
- **Durée d'exécution** : Temps de simulation (actuellement 10 secondes)