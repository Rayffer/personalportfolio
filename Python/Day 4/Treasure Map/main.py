print("Welcome to the Treasure Map")

row_1 = ["🗌", "🗌", "🗌"]
row_2 = ["🗌", "🗌", "🗌"]
row_3 = ["🗌", "🗌", "🗌"]

map = [ row_1, row_2, row_3 ]
print(f"{row_1}\n{row_2}\n{row_3}")

position = input("Where do you want to put the treasure? (Enter X and Y coordinates):")

map[int(position[1]) - 1][int(position[0]) - 1] = "❌"

print(f"{row_1}\n{row_2}\n{row_3}")