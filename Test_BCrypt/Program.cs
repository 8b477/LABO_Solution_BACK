
string pwd1 = "Hello";

string pwd2 = BC.EnhancedHashPassword(pwd1, 13);


Console.WriteLine(pwd2);
Console.WriteLine(BC.EnhancedVerify("Hello",pwd2)); // True
Console.WriteLine(BC.EnhancedVerify("hello",pwd2)); // False

//$2a$13$MPnPRTvIkqrFkwKW6XY7HuGA7TVeT2zA73PjcSl5ANp1pC2wwizU.