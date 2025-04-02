import csv
import os, re, random
from flask import Flask
from flask_sqlalchemy import SQLAlchemy

app = Flask(__name__)

app.config['SQLALCHEMY_DATABASE_URI'] = 'postgresql://cereal_user:yourpassword@localhost/cereal_db'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False # Not necessary - better performance

db = SQLAlchemy()
db.init_app(app)

class Cereal(db.Model):
    """Represents a cereal product in the database."""
    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String(50), nullable=False)
    mfr = db.Column(db.String(50), nullable=False)
    type = db.Column(db.String(50), nullable=False)
    calories = db.Column(db.Integer, nullable=False)
    protein = db.Column(db.Integer, nullable=False)
    fat = db.Column(db.Integer, nullable=False)
    sodium = db.Column(db.Integer, nullable=False)
    fiber = db.Column(db.Float, nullable=False)
    carbo = db.Column(db.Float, nullable=False)
    sugars = db.Column(db.Integer, nullable=False)
    potass = db.Column(db.Integer, nullable=False)
    vitamins = db.Column(db.Integer, nullable=False)
    shelf = db.Column(db.Integer, nullable=False)
    weight = db.Column(db.Float, nullable=False)
    cups = db.Column(db.Float, nullable=False)
    rating = db.Column(db.Float, nullable=False)
    image_path = db.Column(db.String(200), nullable=True)

    price = db.Column(db.Integer, nullable=False)
    storage = db.Column(db.Integer, nullable=False)

def generate_price():
    return round(random.uniform(20, 50), 2)

def generate_storage():
    return random.randint(0, 10)

def import_csv_data():
    """Reads data from the CSV file and inserts it into the database."""
    try:
        with open('data/Cereal.csv', mode='r', encoding='utf-8') as file:
            csv_reader = csv.reader(file, delimiter=';')
            
            # Skip the first two rows (headers + data types)
            next(csv_reader)
            next(csv_reader)
            
            for row in csv_reader:
                try:
                    cereal_name = row[0].strip()
                    image_path = find_image_for_cereal(cereal_name)

                    price = generate_price()
                    storage = generate_storage()
                    
                    cereal = Cereal(
                        name=cereal_name,
                        mfr=row[1].strip(),
                        type=row[2].strip(),
                        calories=int(row[3]),
                        protein=int(row[4]),
                        fat=int(row[5]),
                        sodium=int(row[6]),
                        fiber=float(row[7]),
                        carbo=float(row[8]),
                        sugars=int(row[9]),
                        potass=int(row[10]),
                        vitamins=int(row[11]),
                        shelf=int(row[12]),
                        weight=float(row[13]),
                        cups=float(row[14]),
                        rating=float(row[15].replace('.', '')),
                        image_path=image_path,

                        price=price,
                        storage=storage

                    )
                    db.session.add(cereal)
                except (IndexError, ValueError) as e:
                    print(f"Skipping row due to error: {e}")

            db.session.commit()
            #db.session.close()
            print("CSV data imported successfully!")

    except FileNotFoundError:
        print("Error: CSV file not found.")
    except Exception as e:
        print(f"Unexpected error: {e}")

def normalize_name(name):
    """Removes spaces, special characters, and converts to lowercase to match file names - for finding images."""
    name = name.lower()
    name = re.sub(r'[^a-z0-9]', '', name)  # Remove non-alphanumeric characters
    return name

def find_image_for_cereal(cereal_name):
    """Finds the best matching image file based on the product name."""
    image_folder = os.path.join("static", "images")
    
    # Ensure the image folder exists
    if not os.path.exists(image_folder):
        print(f"Warning: Image folder '{image_folder}' not found.")
        return None

    normalized_cereal_name = normalize_name(cereal_name)

    # Iterate through all files in static/images/
    for filename in os.listdir(image_folder):
        normalized_filename = normalize_name(os.path.splitext(filename)[0])  # Remove file extension (.jpg, .png)

        if normalized_cereal_name == normalized_filename:  # Compare normalized names
            return (filename)  # Return full path to the image

    return None  # No match found
