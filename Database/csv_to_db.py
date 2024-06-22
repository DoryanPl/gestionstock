import csv
from mysql.connector import connect
import re
import random
from tqdm import tqdm

db = connect(
    host="185.200.244.250",
    port="3306",
    user="u1_Keuwdb9gvH",
    password="2cAL69FjY^USWH=@aG6=QPnL",
    database="s1_projet_cours"
)

try:
    # Table records deletion
    cursor = db.cursor()

    cursor.execute('DELETE FROM materiel WHERE id_materiel > 0')
    db.commit()

    with open('../docs/BDD/Basearticlegestion_csv_file.csv', 'r') as file:
        spamreader = list(csv.reader(file, delimiter=';'))
        i = -1
        for i in tqdm(range(1, len(spamreader)), desc='Loading...'):
            row = spamreader[i]

            col_caracteristique = re.sub(
                r"([\'])", r'\\\1', r"{}".format(row[1]))
            col_tva = float(''.join(''.join(row[4][1:].split('?')).split(',')))

            col_prix_ht = float(
                row[4].replace('?', '').replace(',', '.').replace('€', '').strip())
            col_prix_ttc = float(
                row[5].replace('?', '').replace(',', '.').replace('€', '').strip())

            col_qt = random.randrange(1, 10)
            col_ref = row[0]
            col_categories = row[2]

            if col_tva != 0:
                tva = col_prix_ttc / col_prix_ht
            else:
                tva = 0

            cursor = db.cursor()

            insert_query = """
              INSERT INTO materiel
              (id_materiel, reference, categorie, quantite,
              caracteristique, flash_code, prix_ht, tva, prix_ttc)
              VALUES ({ite}, '{ref}', '{categories}', {quantite},
              '{caracteristiques}', NULL, {prix_ht}, {tva}, {prix_ttc})
              """.format(ite=i, ref=col_ref, categories=col_categories, quantite=col_qt, caracteristiques=col_caracteristique, prix_ht=col_prix_ht, tva=tva, prix_ttc=col_prix_ttc)

            cursor.execute(insert_query)
            db.commit()

except Exception as err:
    print(err)
    exit()
except KeyboardInterrupt:
    print('Le programme se ferme')
    exit()

if db.is_connected():
    db.disconnect()
