//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.2.0.0 (NJsonSchema v11.1.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

export class Client {
  private http: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> }
  private baseUrl: string
  protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined

  constructor(
    baseUrl?: string,
    http?: { fetch(url: RequestInfo, init?: RequestInit): Promise<Response> },
  ) {
    this.http = http ? http : (window as any)
    this.baseUrl = baseUrl ?? ''
  }

  /**
   * @param body (optional)
   * @return OK
   */
  register(body: UserCreateDTO | undefined): Promise<UserCreateDTO> {
    let url_ = this.baseUrl + '/api/User/register'
    url_ = url_.replace(/[?&]$/, '')

    const content_ = JSON.stringify(body)

    let options_: RequestInit = {
      body: content_,
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Accept: 'text/plain',
      },
    }

    return this.http.fetch(url_, options_).then((_response: Response) => {
      return this.processRegister(_response)
    })
  }

  protected processRegister(response: Response): Promise<UserCreateDTO> {
    const status = response.status
    let _headers: any = {}
    if (response.headers && response.headers.forEach) {
      response.headers.forEach((v: any, k: any) => (_headers[k] = v))
    }
    if (status === 200) {
      return response.text().then((_responseText) => {
        let result200: any = null
        result200 =
          _responseText === ''
            ? null
            : (JSON.parse(_responseText, this.jsonParseReviver) as UserCreateDTO)
        return result200
      })
    } else if (status !== 200 && status !== 204) {
      return response.text().then((_responseText) => {
        return throwException(
          'An unexpected server error occurred.',
          status,
          _responseText,
          _headers,
        )
      })
    }
    return Promise.resolve<UserCreateDTO>(null as any)
  }

  /**
   * @param username (optional)
   * @param password (optional)
   * @return OK
   */
  logIn(username: string | undefined, password: string | undefined): Promise<UserDataDTO> {
    let url_ = this.baseUrl + '/api/User/log-in?'
    if (username === null) throw new Error("The parameter 'username' cannot be null.")
    else if (username !== undefined) url_ += 'username=' + encodeURIComponent('' + username) + '&'
    if (password === null) throw new Error("The parameter 'password' cannot be null.")
    else if (password !== undefined) url_ += 'password=' + encodeURIComponent('' + password) + '&'
    url_ = url_.replace(/[?&]$/, '')

    let options_: RequestInit = {
      method: 'POST',
      headers: {
        Accept: 'text/plain',
      },
    }

    return this.http.fetch(url_, options_).then((_response: Response) => {
      return this.processLogIn(_response)
    })
  }

  protected processLogIn(response: Response): Promise<UserDataDTO> {
    const status = response.status
    let _headers: any = {}
    if (response.headers && response.headers.forEach) {
      response.headers.forEach((v: any, k: any) => (_headers[k] = v))
    }
    if (status === 200) {
      return response.text().then((_responseText) => {
        let result200: any = null
        result200 =
          _responseText === ''
            ? null
            : (JSON.parse(_responseText, this.jsonParseReviver) as UserDataDTO)
        return result200
      })
    } else if (status !== 200 && status !== 204) {
      return response.text().then((_responseText) => {
        return throwException(
          'An unexpected server error occurred.',
          status,
          _responseText,
          _headers,
        )
      })
    }
    return Promise.resolve<UserDataDTO>(null as any)
  }

  /**
   * @return OK
   */
  getUserList(): Promise<UserDataDTO[]> {
    let url_ = this.baseUrl + '/api/User/get-user-list'
    url_ = url_.replace(/[?&]$/, '')

    let options_: RequestInit = {
      method: 'GET',
      headers: {
        Accept: 'text/plain',
      },
    }

    return this.http.fetch(url_, options_).then((_response: Response) => {
      return this.processGetUserList(_response)
    })
  }

  protected processGetUserList(response: Response): Promise<UserDataDTO[]> {
    const status = response.status
    let _headers: any = {}
    if (response.headers && response.headers.forEach) {
      response.headers.forEach((v: any, k: any) => (_headers[k] = v))
    }
    if (status === 200) {
      return response.text().then((_responseText) => {
        let result200: any = null
        result200 =
          _responseText === ''
            ? null
            : (JSON.parse(_responseText, this.jsonParseReviver) as UserDataDTO[])
        return result200
      })
    } else if (status !== 200 && status !== 204) {
      return response.text().then((_responseText) => {
        return throwException(
          'An unexpected server error occurred.',
          status,
          _responseText,
          _headers,
        )
      })
    }
    return Promise.resolve<UserDataDTO[]>(null as any)
  }

  /**
   * @param id (optional)
   * @return OK
   */
  getUser(id: string | undefined): Promise<UserDataDTO> {
    let url_ = this.baseUrl + '/api/User/get-user?'
    if (id === null) throw new Error("The parameter 'id' cannot be null.")
    else if (id !== undefined) url_ += 'id=' + encodeURIComponent('' + id) + '&'
    url_ = url_.replace(/[?&]$/, '')

    let options_: RequestInit = {
      method: 'GET',
      headers: {
        Accept: 'text/plain',
      },
    }

    return this.http.fetch(url_, options_).then((_response: Response) => {
      return this.processGetUser(_response)
    })
  }

  protected processGetUser(response: Response): Promise<UserDataDTO> {
    const status = response.status
    let _headers: any = {}
    if (response.headers && response.headers.forEach) {
      response.headers.forEach((v: any, k: any) => (_headers[k] = v))
    }
    if (status === 200) {
      return response.text().then((_responseText) => {
        let result200: any = null
        result200 =
          _responseText === ''
            ? null
            : (JSON.parse(_responseText, this.jsonParseReviver) as UserDataDTO)
        return result200
      })
    } else if (status !== 200 && status !== 204) {
      return response.text().then((_responseText) => {
        return throwException(
          'An unexpected server error occurred.',
          status,
          _responseText,
          _headers,
        )
      })
    }
    return Promise.resolve<UserDataDTO>(null as any)
  }

  /**
   * @param id (optional)
   * @return OK
   */
  getRefreshToken(id: string | undefined): Promise<string> {
    let url_ = this.baseUrl + '/api/User/get-refresh-token?'
    if (id === null) throw new Error("The parameter 'id' cannot be null.")
    else if (id !== undefined) url_ += 'id=' + encodeURIComponent('' + id) + '&'
    url_ = url_.replace(/[?&]$/, '')

    let options_: RequestInit = {
      method: 'GET',
      headers: {
        Accept: 'text/plain',
      },
    }

    return this.http.fetch(url_, options_).then((_response: Response) => {
      return this.processGetRefreshToken(_response)
    })
  }

  protected processGetRefreshToken(response: Response): Promise<string> {
    const status = response.status
    let _headers: any = {}
    if (response.headers && response.headers.forEach) {
      response.headers.forEach((v: any, k: any) => (_headers[k] = v))
    }
    if (status === 200) {
      return response.text().then((_responseText) => {
        let result200: any = null
        result200 =
          _responseText === '' ? null : (JSON.parse(_responseText, this.jsonParseReviver) as string)
        return result200
      })
    } else if (status !== 200 && status !== 204) {
      return response.text().then((_responseText) => {
        return throwException(
          'An unexpected server error occurred.',
          status,
          _responseText,
          _headers,
        )
      })
    }
    return Promise.resolve<string>(null as any)
  }
}

export interface UserCreateDTO {
  userName: string | undefined
  name: string | undefined
  surname: string | undefined
  email: string | undefined
  password: string | undefined
  role: string | undefined
  createdAt?: Date
}

export interface UserDataDTO {
  id?: string | undefined
  userName?: string | undefined
  name?: string | undefined
  surname?: string | undefined
  email?: string | undefined
  role?: string | undefined
  createdAt?: Date
}

export class ApiException extends Error {
  message: string
  status: number
  response: string
  headers: { [key: string]: any }
  result: any

  constructor(
    message: string,
    status: number,
    response: string,
    headers: { [key: string]: any },
    result: any,
  ) {
    super()

    this.message = message
    this.status = status
    this.response = response
    this.headers = headers
    this.result = result
  }

  protected isApiException = true

  static isApiException(obj: any): obj is ApiException {
    return obj.isApiException === true
  }
}

function throwException(
  message: string,
  status: number,
  response: string,
  headers: { [key: string]: any },
  result?: any,
): any {
  if (result !== null && result !== undefined) throw result
  else throw new ApiException(message, status, response, headers, null)
}
