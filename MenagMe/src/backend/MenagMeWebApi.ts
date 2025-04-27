import { Client } from './BaseApi'

export class MenagMeWebApi extends Client {
  userTokenResult: any

  constructor(url: string) {
    const http: unknown = {
      fetch: null,
    }
    super(url, http as ApiHttpClient)
    ;(http as ApiHttpClient).fetch = this.fetchWrapper.bind(this)

    this.restoreTokens()
  }

  restoreTokens() {
    const rawAuthData = window.localStorage.getItem('loggedUser')
    if (rawAuthData) {
      const parsedData = JSON.parse(rawAuthData)
      this.userTokenResult = parsedData.token
    }
  }
  private async fetchWrapper(requestInfo: RequestInfo, init?: RequestInit): Promise<Response> {
    const opts: RequestInit = {
      ...init,
    }

    if (this.userTokenResult) {
      opts.headers = {
        ...opts.headers,
        Authorization: `Bearer ${this.userTokenResult}`,
      }
    }

    const request = fetch(requestInfo, opts)
    return request
  }
}

type ApiHttpClient = {
  fetch(url: RequestInfo, init?: RequestInit): Promise<Response>
}
