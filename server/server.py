from flask import Flask, request, jsonify

app = Flask(__name__)

@app.route('/')
def index():
    return jsonify({'foo': 'bar'})

@app.route('/test')
def test():
	return jsonify({'hello': 'world'})

if __name__ == "__main__":
    app.run(debug=True)