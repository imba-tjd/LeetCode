typedef struct {
    // User defined data may be declared here.
    volatile int a;
    volatile int b;
} Foo;

Foo* fooCreate() {
    Foo* obj = (Foo*) malloc(sizeof(Foo));

    // Initialize user defined data here.
    obj->a=obj->b=0;

    return obj;
}

void first(Foo* obj) {
    // printFirst() outputs "first". Do not change or remove this line.
    printFirst();

    obj->a = 1;
}

void second(Foo* obj) {
    while(obj->a == 0)
        usleep(1);
    // printSecond() outputs "second". Do not change or remove this line.
    printSecond();

    obj->b = 1;
}

void third(Foo* obj) {
    while(obj->b == 0)
        usleep(1);
    // printThird() outputs "third". Do not change or remove this line.
    printThird();
}

void fooFree(Foo* obj) {
    // User defined data may be cleaned up here.
    free(obj);
}
