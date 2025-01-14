export class AccessDeniedError extends Error {
  redirectUri: string | undefined;
  constructor(msg: string, redirect?: string) {
    super(msg);
    this.redirectUri = redirect;
  }
}

export class AuthenticationError extends Error {
  redirectUri: string | undefined;
  constructor(msg: string, redirect?: string) {
    super(msg);
    this.redirectUri = redirect;
  }
}

export class NotFoundError extends Error {
  redirectUri: string | undefined;
  constructor(msg: string, redirect?: string) {
    super(msg);
    this.redirectUri = redirect;
  }
}
