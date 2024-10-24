# INTRO

**PAS** est un jeu de combat en tour par tour développé dans le contexte d'un exercice à réaliser au CNAM ENJMIN. Dans ce README nous allons expliquer le gameplay et décrire son fonctionnement de manière plus ou moins détaillée,
à noter qu'il est parfois un peu mal optimisé et que les outils développés pour ce projet sont souvent peu ergonomiques.

*Les deux améliorations choisies pour le projet sont l'interface graphique et la classe supplémentaire.*

# GAMEPLAY

Le gameplay décrit dans les consignes est le suivant : Un jeu en tour par tour, avec 3 classes jouables possédant une stat de vie, de puissance (dégats) et une capacité spéciale.
Nous avons donc 4 classes, dont 3 imposées par les consignes et une entrant dans le cadre d'une des améliorations à apporter au gameplay et proposée par les consignes :

Le **Damager** : *3 de vide, 2 de puissance, capacité : au prochain tour, renvoie les dégats à l'envoyeur mais les prends quand même, avec un cooldown de 3 tours.*
Le **Healer** : *4 de vide, 1 de puissance, capacité : restore 2 coeurs de santé, avec un cooldown de 2 tours.*
Le **Tank** : *5 de vide, 1 de puissance, capacité : augmente sa puissance de 1 pour le prochain tour, au prix d'un point de vie, avec un cooldown de 2 tours.*
Le **Wukong Bébère** : *3 de vide, 1 de puissance, capacité : échape à la mort au prochain tour, avec un coeur et 1 de dégats en plus, avec un cooldown de 4 tours.*

Le joueur commence, puis l'IA joue etc... à noter que l'IA fonctionne aléatoirement.

# FONCTIONNEMENT

## Moteur

Le jeu utilise un mini moteur de jeux fait maison basé sur SFML.Net (bindings de SFML pour C#) en version 2.2 avec la librairie NetEXT de SFML pour les animations (portage de Thor pour C#).

La classe principale est la classe Game. C'est un singleton qui gère la gestion de scène(s) (en executant les fonctions Tick associées), l'affichage, et qui possède différentes propriétés utiles (delta time par exemple.)
Cette classe gère commme dit ci dessus des *scènes*. En effet avec SetScene on peut lui passer un objet d'un type héritant de la classe Scene. Une scène possède une liste d'**acteurs**, et des fonctions virtuelles Start, Tick, et End (rarement utilisée)

les **Acteurs** sont des objets dans la scène, possédant un sprite pour l'affichage, une variable parentScene pour accéder à la scène qui contient l'objet et des fonctions virtuelles Start et Tick.
On peut aussi override les fonctions d'affichage (Draw) et la fonction SetPosition au cas où
l'acteur aurait d'autres éléments visuels, et possède une fonction Innit qui s'éxecute à l'ajout de l'objet dans la scène, avec la position voulue de l'objet et la scène parente en paramètre.

De là découlent toutes les autres fonctionnalités, comme la classe **Button** et la class **Character** (correspondant à la base d'un personnage jouable) héritant tous deux de **Actor**.

Dans **Character** on a les fonctions virtuelles **Ability**, **Attack**, **OnRecieveDamage**, **OnParry**...

## Jeu

On a différentes scène qui sont hors du sujet du devoir, la scène qui nous intéresse ici et qui contient la boucle de gameplay est la scène **CombatScene** *(Content/Scenes/CombatScene.cs)*.

Dans le constructeur on initialise les objets nécessaire au jeu.

Dans la fonction Tick on a uniquement le comportement de l'IA, puisque le joueur est lui géré à l'appuie d'un bouton avec un event de c# (dans la fonction **PlayerAction**, bind à l'event **Clicked** des boutons Attack, Parry et Ability)
On vérifie d'abord si ce n'est pas le tour de l'IA ou que le jeu est en pause, et dans ce cas là on quitte la fonction pour empêcher l'IA de jouer. Puis on choisis une action aléatoire et on essaye de l'exécuter, et si ça rate on recommence.
Si ça réussit on peut donc indiquer au jeu que c'est le tour du joueur en mettant IsPlayerTurn à true, on execute les méthodes OnTurnComplete des deux joueurs et la fonction est finie.

Pour le joueur c'est le même fonctionnement si ce n'est que l'action n'est pas choisie aléatoirement mais en fonction du bouton pressé par le joueur.

Enfin quand un joueur perd toute sa vie un event est appelé qui exécute le script de victoire ou de défaite en fonction, en mettant sur pause le jeu, en supprimant les boutons d'action de la scène, en ajoutant un bouton vers le menu
principal et en ajoutant un texte en fonction de la victoire ou de la défaite.
