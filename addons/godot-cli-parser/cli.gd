#!/usr/bin/env -S godot -s
extends SceneTree
# https://docs.godotengine.org/en/stable/tutorials/editor/command_line_tutorial.html

class_name parser

static func run(cmds: Node) -> Error:
	var args: Dictionary = get_args()
	for command in cmds.get_method_list():
		if command["name"] == args["command"]:
			return call_method(cmds, command, args)
	return ERR_METHOD_NOT_FOUND
	

static func call_method(c: Node, command: Dictionary, args: Dictionary) -> Error:
	var arg_num:int = c.get_method_argument_count(command["name"])
	var inp_num:int = args["args"].size()

	# Not enough arguments
	if arg_num > inp_num:
		return ERR_INVALID_PARAMETER
	# Discard extra provided arguments
	var inputs: Array = []
	for i in range(inp_num):
		if i >= arg_num:
			break
		inputs.append( args["args"][i])
		
	# type check arguments
	for i in range(command["args"].size()):
		if ! is_instance_of( inputs[i], command["args"][i]["type"]):
			return ERR_INVALID_PARAMETER
			# return "Type of \"" + inputs[i] + "\" was incorrect, expected " + type_string(command["args"][i]["type"])
	
	c.callv(command["name"], inputs)
	return OK


static func get_args() -> Dictionary:
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




