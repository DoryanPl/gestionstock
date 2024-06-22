# Projet gestion stock

Le projet gestion stock à pour but de créer une application Windows, une application web et une application raspberry.

## :computer: Fonctionnalités des applications

### Fonctionnalités de l’application Windows

- Ajouter, modifier ou supprimer un client de la base de données
- Ajouter, modifier ou supprimer un employé de la base de données
- Ajouter, modifier ou supprimer un matériel de la base de données
- Générer une fiche de prestation, un devis ou une facture et l’archiver sur le serveur
- Créer et associer un QR Code à chaque matériel

### Fonctionnalités de l’application Web

- La lecture du QR Code d’un matériel
- L’affichage d’une fiche de prestation, d’un devis ou d’une facture, sauvegardés sur le serveur
- Mise à jour de la base de données

### Fonctionnalités de l'application Raspberry

- L’acquisition de la température et de l’humidité toutes les 4 heures
- L’archivage des mesures ci-dessus dans la base de données.

> :bulb: Pour plus de renseignements sur le projet consulter le [cahier des charges](docs/cahier_des_charges.pdf).

## :file_folder: Description des différents dossiers

### Le dossier `database`

Ce dossier contient le script d'installation de la base de données SQL.


### Le dossier `docs`

Ce dossier contient tous les documents utile au développement des applications du projet.


### Le dossier `Gestionnaire`

Ce dossier contient l'application WPF - Gestionnaire.

## Les fonctions à développer et tâches à effectuer des différents étudiants

### :spades: Etudiant 1

- S’approprier la base de données fournie
- Créer l’application Windows conjointement avec l’étudiant 2
- Créer la fenêtre d’identification
- Créer les fenêtres de gestion d’un matériel, un client et un employé
- Générer un QRCode
- Imprimer le QRCode généré.
- Intégrer avec les étudiants 2 et 3

### :hearts: Etudiant 2

- S’approprier la base de données fournie
- Créer l’application Windows conjointement avec l’étudiant 1
- Créer les fenêtres de gestion d’un client et d’un employé
- Créer la fenêtre de gestion d’une facture
- Créer la fenêtre de gestion d’un devis
- Créer la fenêtre de génération d’une fiche de livraison
- Intégrer avec les étudiants 1 et 3

### :clubs: Etudiant 3

- S’approprier la base de données fournie
- Créer le module d’accès à la base de données pour l’application Web
- Crée l’application Web
- Lire un QRCode
- Afficher les informations sur le matériel associé
- Afficher la liste de livraison dans un tableau
- Mettre à jour la base de données

### :diamonds: Etudiant 4

- S’approprier la base de données fournie
- Créer l’application Raspberry
- Créer le module d’accès à la base de données
- Gérer le capteur DHT22
- Faire l’acquisition de la température et de l’humidité toutes les 4h
- Mettre à jour la base de données
