# ExpenseAPI

Lancement de l'application
Si vous souhaitez lancez l'application, n'oubliez pas de générer un certificat avant :
dotnet dev-certs https --trust

Utilisation de l'application :
J'ai pris le parti de considérer que l'utilisateur et la devise sont enregistrée dans des étapes précédentes puis proposés dans l'IHM à la création des dépenses.
Des controllers permettent ces ajouts pour vos tests, même si ils en contiennent pas de contrôles, pour éviter des doublons par exemple.

En ce qui concerne les tris, on peut trier par date et par montant pour un utilisateur donné.
Pour l'ensemble des utilisateurs, on peut trier par date mais pas par montant car, comme ces montants peuvent être dans des devises différentes, cela n'avait pas de sens pour moi. Cela peut par contre être une idée d'évolution, avec une conversion des montants dans une devise de référence.

Exemple d'utilisation :

    Ajout d'une devise :
        Post localhost:7021/api/currencies
            {
                "name": "Icelandic krona",
                "symbol": "kr"
            }
        Le retour contient l'id de la nouvelle devise
    
    Ajout d'un utilisateur :
        Post localhost:7021/api/users
            {
                "firstName": "Thor",
                "lastName": "Odinson",
                "currencyId": 4
            }

    Ajout d'une dépense :
        Post localhost:7021/api/expenses
            {
                "userId": 8,
                "date": "2022-06-03",
                "nature": "Restaurant",
                "amount": {
                    "amount": 154.57,
                    "currencyId": 4
                },
                "comment": "un bon banquet au Valhalla"
            }

Vous pouvez ajouter plusieurs dépenses pour tester ensuite les différentes listes.

Bonne lecture

La playlist qui m'a accompagné pendant cet exercice :
Fakear :https://www.youtube.com/watch?v=7BIYXR2QsfU
Chapelier fou : https://www.youtube.com/watch?v=40kUiKMynhk
MF Robots : https://www.youtube.com/watch?v=wbPU9ujhfs8&list=RDEM-jyE4XByB6q6q-lA5x5c-A&start_radio=1
Digresk : https://www.youtube.com/watch?v=fFNPyasQelA&list=RDEMV5jbFGuOtt7GfC2re8d3Yw&start_radio=1
Ibrahim Maalouf : https://www.youtube.com/watch?v=le-e37ZMck8&list=TLPQMDIwNjIwMjKDufqQ0uZ1QQ&index=11