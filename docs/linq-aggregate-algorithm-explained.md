# LINQ Aggregate algorithm explained
The easiest-to-understand definition of Aggregate is that it performs an operation on each element of the list taking into account the operations that have gone before. That is to say it performs the action on the first and second element and carries the result forward. Then it operates on the previous result and the third element and carries forward. etc. <br/><br/>
> **Example 1. Summing numbers**
> ```
> var nums = new[]{1,2,3,4};
> var sum = nums.Aggregate( (a,b) => a + b);
> Console.WriteLine(sum); // output: 10 (1+2+3+4)
> ```
> This adds 1 and 2 to make 3. Then adds 3 (result of previous) and 3 (next element in sequence) to make 6. Then adds 6 and 4 to make 10.

<br/>

> **Example 2. create a csv from an array of strings**
> ```
> var chars = new []{"a","b","c", "d"};
> var csv = chars.Aggregate( (a,b) => a + ',' + b);
> Console.WriteLine(csv); // Output a,b,c,d
> ```
> This works in much the same way. Concatenate `a` a comma and `b` to make `a,b`. Then concatenates `a,b` with a comma and `c` to make `a,b,c`. and so on.

<br/>

> **Example 3. Multiplying numbers using a seed**<br/>
> For completeness, there is an overload of Aggregate which takes a seed value.
> ```
> var multipliers = new []{10,20,30,40};
> var multiplied = multipliers.Aggregate(5, (a,b) => a * b);
> Console.WriteLine(multiplied); //Output 1200000 ((((5*10)*20)*30)*40)
> ```

Much like the above examples, this starts with a value of 5 and multiplies it by the first element of the sequence 10 giving a result of 50. This result is carried forward and multiplied by the next number in the sequence 20 to give a result of 1000. This continues through the remaining 2 element of the sequence.

[More...](https://stackoverflow.com/a/7105616/16981619)