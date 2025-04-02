import csv
import os, re, random
import psycopg2

def generate_price():
    return round(random.uniform(20, 50), 2)

def generate_storage():
    return random.randint(0, 10)

def generate_manufacturer(manufacturer):
    match manufacturer:
        case "A":
            return "American Home Food Products"
        case "G":
            return "General Mills"
        case "K":
            return "Kelloggs"
        case "N":
            return "Nabisco"
        case "P":
            return "Post"
        case "Q":
            return "Quaker Oats"
        case "R":
            return "Ralston Purina"
        case default:
            pass

def parse_csv():
    try:
        conn = psycopg2.connect(
            dbname="cerealdatabase",
            user="postgres",
            password="250101",
            host="localhost"
        )
        cursor = conn.cursor()

        with open('parser/data/Cereal.csv', mode='r', encoding='utf-8') as file:
            csv_reader = csv.reader(file, delimiter=';')
            rows = list(csv_reader)
            keys = rows[0]

            for row in rows[2:]:
                row_dict = dict(zip(keys, row))
                row_dict['manufacturer'] = generate_manufacturer(row_dict['manufacturer'])
                row_dict['price'] = generate_price()
                row_dict['storage'] = generate_storage()
                row_dict['image_path'] = find_image_for_cereal(row_dict['name'])
                
                # Remove '.' from rating value
                row_dict['rating'] = row_dict['rating'].replace('.', '')

                # Prepare query
                columns = ', '.join([key for key in row_dict.keys() if key != 'id'])
                placeholders = ', '.join(['%s'] * (len(row_dict)))
                values = [value for key, value in row_dict.items() if key != 'id']

                query = f"INSERT INTO products ({columns}) VALUES ({placeholders})"
                cursor.execute(query, values)

        conn.commit()
        cursor.close()
        conn.close()
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
    image_folder = os.path.join("parser","static", "images")
    
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

if __name__ == "__main__":
    parse_csv()