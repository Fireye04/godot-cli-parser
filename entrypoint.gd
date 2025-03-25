extends Node

class c2:
	extends Node
	func test (bob: String, testy: int) -> void:
		print(bob)
		print(testy+1)

func _ready() -> void:
	var c = c2.new()
	var result: Dictionary = parser.run(c)
	c.queue_free()
	if result.error:
		print(error_string(result.error) + ": " + result.message)
		get_tree().quit()

