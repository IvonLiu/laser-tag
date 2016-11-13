import time
from flask import Flask, request, jsonify

curr_time_millis = lambda: int(round(time.time() * 1000))
respawn_time = 30*1000

app = Flask(__name__)
players = {}


@app.route('/')
def index():
    return jsonify({'foo': 'bar'})

@app.route('/test')
def test():
	return jsonify({'hello': 'world'})

@app.route('/check', defaults={'name': ''})
@app.route('/check/<name>')
def check(name):
	print(name)
	if name in players:
		if curr_time_millis() - players[name] > respawn_time:
			del players[name]
			return 'false'
		else:
			return 'true'
	return 'false'

@app.route('/shoot', defaults={'target': ''})
@app.route('/shoot/<target>')
def shoot(target):
	print(target)
	players[target] = curr_time_millis()
	return 'shot'

if __name__ == "__main__":
    app.run(debug=True)