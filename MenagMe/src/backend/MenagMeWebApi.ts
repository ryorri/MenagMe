import { Client } from './BaseApi'

export class MenagMeWebApi extends Client {
  userTokenResult: any
  refreshTokenResult: any
  userIdResult: any
  user: any

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
      this.user = parsedData.result
      this.userTokenResult = parsedData.token
      this.refreshTokenResult = parsedData.refreshToken
      this.userIdResult = parsedData.result.id
    }
  }
  private async fetchWrapper(requestInfo: RequestInfo, init?: RequestInit): Promise<Response> {
    const opts: RequestInit = {
      ...init,
      headers: {
        ...(init?.headers || {}),
      },
    }

    if (this.userTokenResult) {
      opts.headers = {
        ...opts.headers,
        Authorization: `Bearer ${this.userTokenResult}`,
      }
    }

    let response = await fetch(requestInfo, opts)

    // If unauthorized, try refreshing token
    if (response.status === 401) {
      const newTokens = (await this.getRefreshToken(
        this.userIdResult,
        this.refreshTokenResult,
      )) as any

      if (newTokens) {
        // Update tokens in memory
        this.userTokenResult = newTokens.token
        this.refreshTokenResult = newTokens.newRefreshToken

        // Save updated tokens to localStorage
        window.localStorage.setItem(
          'loggedUser',
          JSON.stringify({
            result: this.user,
            token: newTokens.token,
            refreshToken: newTokens.newRefreshToken,
          }),
        )

        // Retry original request with new token
        opts.headers = {
          ...opts.headers,
          Authorization: `Bearer ${this.userTokenResult}`,
        }

        response = await fetch(requestInfo, opts)
      }
    }

    return response
  }
}

type ApiHttpClient = {
  fetch(url: RequestInfo, init?: RequestInit): Promise<Response>
}
