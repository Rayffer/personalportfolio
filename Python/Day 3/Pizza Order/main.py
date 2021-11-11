print("Welcome to python pizza deliveries")

pizza_size = input("What size of pizza do you want? S, M or L?")
add_pepperoni = input("Do you want pepperoni? Y or N?")
extra_cheese = input("Do you want extra cheese? Y or N?")

price = 0
if pizza_size == 'S':
    price += 15
elif pizza_size == 'M':
    price += 20
elif pizza_size == 'L':
    price += 25

if (add_pepperoni == 'Y'):
    if (pizza_size == 'S'):
        price += 2
    else:
        price += 3

if (extra_cheese == 'Y'):
    price += 2

print(f"You ordered a {pizza_size} pizza, {'with' if (add_pepperoni == 'Y') else 'without'} pepperoni, {'with ' if (add_pepperoni == 'Y') else 'without '} extra cheese, for a cost of {price}€")