extends Node

var money := 0.0
signal money_changed

func add_money(amount: int):
	money += amount
	
func spend_money(amount: int):
	if amount > money:
		return false
		
	money -= amount
	
	return true
