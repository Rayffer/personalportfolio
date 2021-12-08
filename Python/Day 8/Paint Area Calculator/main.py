import math

def convert_to_float(input):
    try:
        return float(input)
    except ValueError:
        print("Please enter a float value")
        return ""

def calculate_required_paint_cans( width: float, height: float, coverage_per_can:float):
    return math.ceil(width * height / coverage_per_can)

print("Welcome to the Paint Area Calculator")
width = ""
height = ""
coverage_per_can = ""
while width == "":
    width = convert_to_float(input("How wide is the wall? "))
while height == "":
    height = convert_to_float(input("How tall is the wall? "))
while coverage_per_can == "":
    coverage_per_can = convert_to_float(input("How much area does each can cover? "))

amount_of_cans = calculate_required_paint_cans(width, height, coverage_per_can)

print(f"You will need at least {amount_of_cans} cans to paint the whole wall")