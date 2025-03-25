#!/usr/bin/env -S godot -s
extends SceneTree
# https://docs.godotengine.org/en/stable/tutorials/editor/command_line_tutorial.html

class_name parser

static func run(cmds: Node) -> Dictionary:
	"""Default run command for parser. 

	Parameter
	---------
	cmds: Node
		User defined class containing all methods exposed to the CLI.

	Return
	------
	Dictionary
		"error" : Error
			Error code
		"message" : String
			More detailed error message
	"""
	var args: Dictionary = get_args()
	for command in cmds.get_method_list():
		if command["name"] == args["command"]:
			return call_method(cmds, command, args)
	return {"error":ERR_METHOD_NOT_FOUND, "message": "Method not found in provided commands class"}
	

static func call_method(c: Node, command: Dictionary, args: Dictionary) -> Dictionary:
	"""Attepts to call the provided command with provided args.

	Parameters
	----------
	c : Node
		command parent class
	command : Dictionary
		Dictionary of command metadata provided from Godot's get_method_list()
	args : Dictionary
		output from local get_args() command

	Return
	------
	Dictionary
		"error" : Error
			Error code
		"message" : String
			More detailed error message
	"""
	var arg_num:int = c.get_method_argument_count(command["name"])
	var inp_num:int = args["args"].size()

	# Not enough arguments
	# TODO: Add support for default parameters
	if arg_num > inp_num:
		return {"error": ERR_INVALID_PARAMETER, "message": "Not enough arguments provided"}

	# Discard extra provided arguments
	var inputs: Array = []
	for i in range(inp_num):
		if i >= arg_num:
			break
		inputs.append( args["args"][i])
		
	# type check arguments
	for i in range(command["args"].size()):
		if ! is_instance_of( inputs[i], command["args"][i]["type"]):
			return {"error":ERR_INVALID_PARAMETER, "message": ("Type of \"" +
			str(inputs[i]) + "\" was incorrect, expected " + type_string(command["args"][i]["type"]))}
	
	c.callv(command["name"], inputs)
	return {"error": OK, "message": ""}


static func get_args() -> Dictionary:
	"""Obtains user args from command line

	Return
	------
	Dictionary
		"command" : String
			First provided user argument
		"args" : Array
			List of all following user args
	"""
	var args : PackedStringArray = OS.get_cmdline_user_args()
	if args.size() <= 0:
		return {}
	var cmd = args[0]
	args.remove_at(0)

	var newArgs: Array = []
	for i in range(args.size()):
		if args[i].is_valid_int():
			newArgs.append(args[i].to_int())
			continue
		newArgs.append(args[i])
	
	return {"command": cmd, "args":newArgs}




