extends Node

class c2:
	extends Node
	func test (bob: String, testy: int) -> void:
		print(bob)
		print(testy+1)

func _ready() -> void:
	var c = c2.new()
	var result: Error = parser.run(c)
	c.queue_free()
	if result:
		print(error_string(result))
		queue_free()
		get_tree().quit()

