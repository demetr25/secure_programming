## HW03

#### **** Deadline 2023-10-29 23:59:59 ****

Implement RSA algorithm (key generation, encryption and decryption). Do not use any crypto helper libraries.

Implement brute force decryption, knowing crypted text and public key.

Limit your work within positive LONG bits (64).

No hardcoded values, input validation!!

Find bigest longint n value, that your laptop can decrypt under 5 minutes. Write the value into README.md.

### done

If we use the biggest two 9-digit primes p and q (999999937 and 999999929) then the biggest number which I could decrypt by means of brute force under 5 minutes was 9999999999.
I used exactly 9-digit primes because the longest positive LONG INT is 19 digits long and I needed to be sure that value of "n" and "m" wouldn't be broken.
Also, I have to highlight the fact that it is impossible to correctly decrypt cypher using private key due to the fact that I'm limited by usage of only LONG INT when, for example, to decrypt message there is a potential number like 56684518864^654165464 and such a number just simply can't be stored by LONG INT.