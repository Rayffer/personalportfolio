def convert_to_float(input):
    try:
        return float(input)
    except ValueError:
        print("Please enter a float value")
        return ""

height = ""
while(height == ""):
    height = convert_to_float(input("Enter your height in m:"))
weight = ""
while(weight == ""):
    weight = convert_to_float(input("Enter your weight in kg:"))

print(f"Your bmi is: {round(weight / height ** 2, 2)}")