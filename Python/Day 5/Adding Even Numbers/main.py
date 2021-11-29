def convert_to_int(input):
    try:
        return int(input)
    except ValueError:
        print("Please enter an integer value")
        return ""

print("Welcome to the Add Even numbers exercise")

range_start = ""
while range_start == "":
    range_start = convert_to_int(input("Specify an integer starting number"))

range_end = ""
while range_end == "":
    range_end = convert_to_int(input("Specify an integer ending number"))
    if (range_end < range_start):
        print("Please input a larger number than the starting one")
        range_end = ""
    range_end += 1

total = 0

for number in range(range_start, range_end):
    total += 0 if ((number % 2) == 1) else number

print(f"The sum of even numbers from {range_start} to {range_end} is: {total}")