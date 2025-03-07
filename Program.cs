
/**
 * Note: The returned array must be malloced, assume caller calls free().
 */
int* closestPrimes(int left, int right, int* returnSize) {
    bool sieve[1000001] = {false};
    int primes[80000]; // Approximate size for prime numbers under 1,000,000
    int count = 0;
    
    for (long long i = 2; i < 1000001; i++) {
        if (sieve[i]) continue;
        primes[count++] = (int)i;
        for (long long j = i * i; j < 1000001; j += i) sieve[j] = true;
    }
    primes[count++] = INT_MAX;
    
    // Find the lower bound index
    int lb = 0;
    while (lb < count && primes[lb] < left) lb++;
    
    int* res = (int*)malloc(2 * sizeof(int));
    res[0] = -1;
    res[1] = -1;
    *returnSize = 2;
    
    for (int i = lb; i < count - 1; i++) {
        if (res[0] == -1) {
            if (primes[i + 1] <= right) {
                res[0] = primes[i];
                res[1] = primes[i + 1];
            }
        } else {
            if (primes[i + 1] > right) break;
            int diff = res[1] - res[0];
            if (primes[i + 1] - primes[i] < diff) {
                res[0] = primes[i];
                res[1] = primes[i + 1];
            }
        }
    }
    return res;
}
