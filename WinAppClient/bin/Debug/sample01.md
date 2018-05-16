In the following piece of code, 'message' variable printed the correct value in P1, but doesn't print anything in P2. I followed a tutorial for writing this piece of code, and it looks very similar to what he'd written. I'm using Python 3.6. Can someone explain why?

~~~python
def outer_function(msg):
message = msg 
print(message) #P1

def inner_function():
print(message) #P2
return inner_function()
~~~
    