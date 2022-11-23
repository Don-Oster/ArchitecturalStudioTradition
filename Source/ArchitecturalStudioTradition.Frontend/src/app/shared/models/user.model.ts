export class User {

    constructor(
        private readonly userId: number = 0,
        private readonly firstName: string = null,
        private readonly lastName: string = null
    ) {}

    public static Default(): User {
        return new User();
    }

    get id() {
        return this.userId;
    }

    get name() {
        const parts = [this.firstName, this.lastName];

        return parts.filter(p => p).join(' ');
    }
}
